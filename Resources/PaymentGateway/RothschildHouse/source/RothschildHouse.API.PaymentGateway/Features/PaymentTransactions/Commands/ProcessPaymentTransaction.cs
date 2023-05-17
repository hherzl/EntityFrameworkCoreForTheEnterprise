using MediatR;
using RothschildHouse._3P.CityBank.Contracts;
using RothschildHouse._3P.CityBank.Models;
using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Domain.Enums;
using RothschildHouse.API.PaymentGateway.Domain.Exceptions;
using RothschildHouse.API.PaymentGateway.Domain.Notifications;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;

namespace RothschildHouse.API.PaymentGateway.Features.Transactions.Commands
{
#pragma warning disable CS1591
    public class ProcessPaymentTransactionCommandHandler : IRequestHandler<ProcessPaymentTransactionCommand, ProcessPaymentTransactionResponse>
    {
        private readonly RothschildHouseDbContext _dbContext;
        private readonly ICityBankPaymentServicesClient _cityBankPaymentServicesClient;

        public ProcessPaymentTransactionCommandHandler(RothschildHouseDbContext dbContext, ICityBankPaymentServicesClient cityBankPaymentServicesClient)
        {
            _dbContext = dbContext;
            _cityBankPaymentServicesClient = cityBankPaymentServicesClient;
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

            var customer = await _dbContext.GetCustomerByAlienGuidAsync(request.CustomerGuid, tracking: false, include: false, cancellationToken)
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
                ContentType = PaymentTransactionLog.ApplicationJson,
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
                    ContentType = PaymentTransactionLog.ApplicationJson,
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
            }
            else
            {
                paymentTxn.PaymentTransactionStatusId = (short)PaymentTransactionStatus.Denied;

                _dbContext.PaymentTransactionLog.Add(new PaymentTransactionLog
                {
                    PaymentTransactionId = paymentTxn.Id,
                    PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                    LogType = "Response",
                    ContentType = PaymentTransactionLog.ApplicationJson,
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
