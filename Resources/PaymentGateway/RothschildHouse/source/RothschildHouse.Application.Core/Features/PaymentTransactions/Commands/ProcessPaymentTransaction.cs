using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using MediatR;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Services;
using RothschildHouse.Application.Core.Services.Models;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Domain.Core.Exceptions;
using RothschildHouse.Domain.Core.Notifications;
using RothschildHouse.TP.CityBank.Contracts;
using RothschildHouse.TP.CityBank.Contracts.DataContracts;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.Commands
{
    public record ProcessPaymentTransactionCommand : IRequest<ProcessPaymentTransactionResponse>, IValidatableObject
    {
        public ProcessPaymentTransactionCommand()
        {
        }

        public Guid? ClientApplication { get; set; }
        public Guid? CustomerGuid { get; set; }
        public int? StoreId { get; set; }

        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public Guid? OrderGuid { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string Notes { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ClientApplication == null)
                yield return new ValidationResult("The client application is required", new string[] { nameof(ClientApplication) });

            if (!CustomerGuid.HasValue)
                yield return new ValidationResult("The customer guid is required", new string[] { nameof(CustomerGuid) });

            if (!StoreId.HasValue)
                yield return new ValidationResult("The store is required", new string[] { nameof(StoreId) });

            if (string.IsNullOrEmpty(IssuingNetwork))
                yield return new ValidationResult("The issuing network is required", new string[] { nameof(IssuingNetwork) });
            else if (IssuingNetwork.Length > 25)
                yield return new ValidationResult("The maximum length for issuing network is 25", new string[] { nameof(IssuingNetwork) });

            if (string.IsNullOrEmpty(CardholderName))
                yield return new ValidationResult("The cardholder name is required", new string[] { nameof(CardholderName) });
            else if (CardholderName.Length > 50)
                yield return new ValidationResult("The maximum length for cardholder name is 50", new string[] { nameof(CardholderName) });

            if (string.IsNullOrEmpty(CardNumber))
                yield return new ValidationResult("The card number is required", new string[] { nameof(CardNumber) });
            else if (CardNumber.Length > 20)
                yield return new ValidationResult("The maximum lenght for card number is 20", new string[] { nameof(CardNumber) });

            if (string.IsNullOrEmpty(ExpirationDate))
                yield return new ValidationResult("The expiration date is required", new string[] { nameof(ExpirationDate) });

            if (string.IsNullOrEmpty(Cvv))
                yield return new ValidationResult("The CVV is required", new string[] { nameof(Cvv) });
            else if (Cvv.Length > 4)
                yield return new ValidationResult("The maximum lenght for CVV is 4", new string[] { nameof(Cvv) });

            if (!OrderGuid.HasValue)
                yield return new ValidationResult("The order guid is required", new string[] { nameof(OrderGuid) });

            if (!OrderTotal.HasValue)
                yield return new ValidationResult("The order total is required", new string[] { nameof(OrderTotal) });

            if (OrderTotal <= 0)
                yield return new ValidationResult("The order total must be greater than zero", new string[] { nameof(OrderTotal) });

            if (string.IsNullOrEmpty(Currency))
                yield return new ValidationResult("The currency is required", new string[] { nameof(Currency) });
        }
    }

    public record ProcessPaymentTransactionResponse : CreatedResponse<long?>
    {
        public ProcessPaymentTransactionResponse()
        {
        }

        public ProcessPaymentTransactionResponse(bool successed, string client, decimal? orderTotal, string currency)
        {
            Successed = successed;
            Client = client;
            OrderTotal = orderTotal;
            Currency = currency;
        }

        public bool Successed { get; set; }
        public string Client { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
    }

    public class ProcessPaymentTransactionCommandHandler : IRequestHandler<ProcessPaymentTransactionCommand, ProcessPaymentTransactionResponse>
    {
        public const string ApplicationJson = "application/json";

        private readonly IRothschildHouseDbContext _dbContext;
        private readonly ICityBankPaymentServicesClient _cityBankPaymentServicesClient;
        private readonly ReportsService _reportsService;

        public ProcessPaymentTransactionCommandHandler(IRothschildHouseDbContext dbContext, ICityBankPaymentServicesClient cityBankPaymentServicesClient, ReportsService reportsService)
        {
            _dbContext = dbContext;
            _cityBankPaymentServicesClient = cityBankPaymentServicesClient;
            _reportsService = reportsService;
        }

        public async Task<ProcessPaymentTransactionResponse> Handle(ProcessPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            var card = await _dbContext
                .GetCardAsync(request.IssuingNetwork, request.CardNumber, cancellationToken: cancellationToken);

            if (card == null)
            {
                card = new Card
                {
                    CardTypeId = request.CardTypeId,
                    IssuingNetwork = request.IssuingNetwork,
                    CardholderName = request.CardholderName,
                    CardNumber = request.CardNumber,
                    ExpirationDate = request.ExpirationDate,
                    Cvv = request.Cvv,
                    Active = true,
                    CreationUser = "api",
                    CreationDateTime = DateTime.Now
                };

                _dbContext.Card.Add(card);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            card.AddNotification(new CardCreationNotification(card));

            var currency = await _dbContext.GetCurrencyAsync(request.Currency)
                ?? throw new RothschildHouseException($"There is no definition for Country with ISO Code '{request.Currency}'");

            var clientApplication = await _dbContext.GetClientApplicationAsync(request.ClientApplication)
                ?? throw new RothschildHouseException($"There is no definition for Client Application with Id '{request.ClientApplication}'");

            var customer = await _dbContext.GetCustomerAsync(request.CustomerGuid, tracking: false, include: false, cancellationToken)
                ?? throw new RothschildHouseException($"There is no data for Customer with Id '{request.CustomerGuid}'");

            var paymentTxn = new PaymentTransaction
            {
                Guid = Guid.NewGuid(),
                ClientFullClassName = typeof(ICityBankPaymentServicesClient).FullName,
                PaymentTransactionStatusId = (short)PaymentTransactionStatus.Requested,
                ClientApplicationId = clientApplication.Id,
                CustomerId = customer.Id,
                StoreId = request.StoreId,
                CardId = card.Id,
                Amount = request.OrderTotal,
                CurrencyId = currency.Id,
                CurrencyRate = currency.Rate,
                PaymentTransactionDateTime = request.TransactionDateTime,
                Notes = request.Notes,
                Active = true,
                CreationUser = "api",
                CreationDateTime = DateTime.Now
            };

            _dbContext.PaymentTransaction.Add(paymentTxn);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var paymentGatewayRequest = new ProcessPaymentRequest
            {
                CardTypeId = request.CardTypeId,
                IssuingNetwork = request.IssuingNetwork,
                CardholderName = request.CardholderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                Cvv = request.Cvv,
                OrderTotal = paymentTxn.Amount,
                Currency = currency.Code
            };

            _dbContext.PaymentTransactionLog.Add(new PaymentTransactionLog
            {
                PaymentTransactionId = paymentTxn.Id,
                PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                LogType = "Request",
                ContentType = ApplicationJson,
                Content = paymentGatewayRequest.ToJson(),
                Active = true,
                CreationUser = "api",
                CreationDateTime = DateTime.Now
            });

            var paymentGatewayResponse = await _cityBankPaymentServicesClient.ProcessPaymentAsync(paymentGatewayRequest);

            if (paymentGatewayResponse.Successed)
            {
                paymentTxn.PaymentTransactionStatusId = (short)PaymentTransactionStatus.Processed;

                var paymentTxnLog = new PaymentTransactionLog
                {
                    PaymentTransactionId = paymentTxn.Id,
                    PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                    LogType = "Response",
                    ContentType = ApplicationJson,
                    Content = paymentGatewayResponse.ToJson(),
                    Active = true,
                    CreationUser = "api",
                    CreationDateTime = DateTime.Now
                };

                _dbContext.PaymentTransactionLog.Add(paymentTxnLog);

                paymentTxnLog.AddNotification(new PaymentTransactionProcessedNotification
                {
                    Guid = paymentTxn.Guid,
                    ClientApplication = clientApplication.Name,
                    Amount = paymentTxn.Amount,
                    Currency = currency.Code
                });

                await _reportsService.AddSaleAsync(new SaleDocument
                {
                    ClientApplicationId = paymentTxn.ClientApplicationId,
                    ClientApplication = clientApplication.Name,
                    IssuingNetwork = card.IssuingNetwork,
                    CardTypeId = card.CardTypeId,
                    CardType = card.CardTypeId == (short)CardType.Debit ? "Debit" : "Credit",
                    Total = (double)paymentTxn.Amount,
                    CurrencyId = currency.Id,
                    Currency = currency.Code,
                    CreatedOn = DateTime.Now
                });
            }
            else
            {
                paymentTxn.PaymentTransactionStatusId = (short)PaymentTransactionStatus.Denied;

                _dbContext.PaymentTransactionLog.Add(new PaymentTransactionLog
                {
                    PaymentTransactionId = paymentTxn.Id,
                    PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                    LogType = "Response",
                    ContentType = ApplicationJson,
                    Content = paymentGatewayResponse.ToJson(),
                    Active = true,
                    CreationUser = "api",
                    CreationDateTime = DateTime.Now
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ProcessPaymentTransactionResponse
            {
                Id = paymentTxn.Id,
                Successed = paymentGatewayResponse.Successed,
                Client = clientApplication.Name,
                OrderTotal = paymentTxn.Amount,
                Currency = currency.Code
            };
        }
    }
}
