using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using MediatR;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Domain.Core.Exceptions;
using RothschildHouse.Domain.Core.Notifications;
using RothschildHouse.Library.Common.Clients;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.SearchEngine;
using RothschildHouse.TP.CityBank.Contracts;
using RothschildHouse.TP.CityBank.Contracts.DataContracts;

namespace RothschildHouse.Application.Core.Features.Transactions.Commands
{
    public record ProcessTransactionCommand : IRequest<ProcessTransactionResponse>, IValidatableObject
    {
        public ProcessTransactionCommand()
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

    public record ProcessTransactionResponse : CreatedResponse<long?>
    {
        public ProcessTransactionResponse()
        {
        }

        public ProcessTransactionResponse(bool successed, string client, decimal? orderTotal, string currency)
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

    public class ProcessTransactionCommandHandler : IRequestHandler<ProcessTransactionCommand, ProcessTransactionResponse>
    {
        public const string APPLICATION_JSON = "application/json";
        public const string REQUEST = "Request";
        public const string RESPONSE = "Response";

        private readonly IRothschildHouseDbContext _dbContext;
        private readonly ICityBankPaymentServicesClient _cityBankPaymentServicesClient;
        private readonly SearchEngineClient _searchEngineClient;

        public ProcessTransactionCommandHandler(IRothschildHouseDbContext dbContext, ICityBankPaymentServicesClient cityBankPaymentServicesClient, SearchEngineClient searchEngineClient)
        {
            _dbContext = dbContext;
            _cityBankPaymentServicesClient = cityBankPaymentServicesClient;
            _searchEngineClient = searchEngineClient;
        }

        public async Task<ProcessTransactionResponse> Handle(ProcessTransactionCommand request, CancellationToken cancellationToken)
        {
            var card = await _dbContext
                .GetCardByAsync(request.IssuingNetwork, request.CardNumber, cancellationToken, false)
                ;

            if (card == null)
            {
                card = new Card
                {
                    CardTypeId = request.CardTypeId,
                    IssuingNetwork = request.IssuingNetwork,
                    CardholderName = request.CardholderName,
                    CardNumber = request.CardNumber,
                    ExpirationDate = request.ExpirationDate,
                    Cvv = request.Cvv
                };

                _dbContext.Card.Add(card);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            card.AddNotification(new CardCreationNotification(card));

            var currency = await _dbContext.GetCurrencyAsync(request.Currency)
                ?? throw new RothschildHouseException($"There is no definition for Country with ISO Code '{request.Currency}'");

            var clientApplication = await _dbContext.GetClientApplicationAsync(request.ClientApplication)
                ?? throw new RothschildHouseException($"There is no definition for Client Application with Id '{request.ClientApplication}'");

            var customer = await _dbContext.GetCustomerAsync(request.CustomerGuid, cancellationToken, false, false)
                ?? throw new RothschildHouseException($"There is no data for Customer with Id '{request.CustomerGuid}'");

            var txn = new Transaction
            {
                Guid = Guid.NewGuid(),
                TransactionDateTime = request.TransactionDateTime ?? DateTime.Now,
                TransactionTypeId = (short)TransactionType.Payment,
                TransactionStatusId = (short)TransactionStatus.Requested,
                ClientApplicationId = clientApplication.Id,
                ClientFullClassName = typeof(ICityBankPaymentServicesClient).FullName,
                CustomerId = customer.Id,
                StoreId = request.StoreId,
                CardId = card.Id,
                Amount = request.OrderTotal,
                CurrencyId = currency.Id,
                CurrencyRate = currency.Rate,
                Notes = request.Notes
            };

            _dbContext.Transaction.Add(txn);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var paymentGatewayRequest = new ProcessPaymentRequest
            {
                CardTypeId = request.CardTypeId,
                IssuingNetwork = request.IssuingNetwork,
                CardholderName = request.CardholderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                Cvv = request.Cvv,
                OrderTotal = txn.Amount,
                Currency = currency.Code
            };

            _dbContext.TransactionLog.Add(new TransactionLog
            {
                TransactionId = txn.Id,
                TransactionStatusId = txn.TransactionStatusId,
                LogType = REQUEST,
                ContentType = APPLICATION_JSON,
                Content = paymentGatewayRequest.ToJson()
            });

            var paymentGatewayResponse = await _cityBankPaymentServicesClient.ProcessPaymentAsync(paymentGatewayRequest);

            if (paymentGatewayResponse.Successed)
            {
                txn.TransactionStatusId = (short)TransactionStatus.Processed;

                var paymentTxnLog = new TransactionLog
                {
                    TransactionId = txn.Id,
                    TransactionStatusId = txn.TransactionStatusId,
                    LogType = RESPONSE,
                    ContentType = APPLICATION_JSON,
                    Content = paymentGatewayResponse.ToJson()
                };

                _dbContext.TransactionLog.Add(paymentTxnLog);

                paymentTxnLog.AddNotification(new TransactionProcessedNotification
                {
                    Id = txn.Id,
                    Guid = txn.Guid,
                    ClientApplication = clientApplication.Name,
                    Amount = txn.Amount,
                    Currency = currency.Code
                });

                await _searchEngineClient.IndexSaleAsync(new IndexSaleRequest
                {
                    TxnId = txn.Id,
                    TxnGuid = txn.Guid,
                    TxnDateTime = txn.TransactionDateTime,
                    ClientApplicationId = txn.ClientApplicationId,
                    ClientApplication = clientApplication.Name,
                    IssuingNetwork = card.IssuingNetwork,
                    CardTypeId = card.CardTypeId,
                    CardType = card.CardTypeId == (short)CardType.Debit ? "Debit" : "Credit",
                    Total = (double)txn.Amount,
                    CurrencyId = currency.Id,
                    Currency = currency.Code
                });
            }
            else
            {
                txn.TransactionStatusId = (short)TransactionStatus.Denied;

                _dbContext.TransactionLog.Add(new TransactionLog
                {
                    TransactionId = txn.Id,
                    TransactionStatusId = txn.TransactionStatusId,
                    LogType = RESPONSE,
                    ContentType = APPLICATION_JSON,
                    Content = paymentGatewayResponse.ToJson()
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ProcessTransactionResponse
            {
                Id = txn.Id,
                Successed = paymentGatewayResponse.Successed,
                Client = clientApplication.Name,
                OrderTotal = txn.Amount,
                Currency = currency.Code
            };
        }
    }
}
