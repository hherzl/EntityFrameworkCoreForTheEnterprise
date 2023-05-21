using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.Queries
{
    public record PaymentTransactionDetailsModel
    {
        public PaymentTransactionDetailsModel()
        {
            Logs = new();
        }

        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public Guid? CustomerId { get; set; }
        public string Customer { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public List<PaymentTransactionLogDetailsModel> Logs { get; set; }
    }

    public record PaymentTransactionLogDetailsModel
    {
        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public string LogType { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }

    public class GetPaymentTransactionQuery : IRequest<SingleResponse<PaymentTransactionDetailsModel>>
    {
        public GetPaymentTransactionQuery()
        {
        }

        public GetPaymentTransactionQuery(long? id)
        {
            Id = id;
        }

        public long? Id { get; set; }
    }

    public class GetPaymentTransactionQueryHandler : IRequestHandler<GetPaymentTransactionQuery, SingleResponse<PaymentTransactionDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<PaymentTransactionDetailsModel>> Handle(GetPaymentTransactionQuery request, CancellationToken cancellationToken)
        {
            var paymentTxn = await _dbContext.GetPaymentTransactionAsync(request.Id, tracking: false, cancellationToken: cancellationToken);

            if (paymentTxn == null)
                return null;

            var paymentTxnStatuses = await _dbContext.VPaymentTransactionStatus.ToListAsync(cancellationToken);
            var cardTypes = await _dbContext.VCardType.ToListAsync(cancellationToken);
            var customer = paymentTxn.CustomerFk.PersonId.HasValue ? paymentTxn.CustomerFk.PersonFk.FullName : paymentTxn.CustomerFk.CompanyFk.Name;

            var detailsModel = new PaymentTransactionDetailsModel
            {
                Id = paymentTxn.Id,
                PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                PaymentTransactionStatus = paymentTxnStatuses.FirstOrDefault(item => item.Id == paymentTxn.PaymentTransactionStatusId)?.Name,
                ClientApplicationId = paymentTxn.ClientApplicationId,
                ClientApplication = paymentTxn.ClientApplicationFk.Name,
                CustomerId = paymentTxn.CustomerId,
                Customer = customer,
                CardType = cardTypes.FirstOrDefault(item => item.Id == paymentTxn.CardFk.CardTypeId)?.Name,
                IssuingNetwork = paymentTxn.CardFk.IssuingNetwork,
                CardholderName = paymentTxn.CardFk.CardholderName,
                OrderTotal = paymentTxn.Amount,
                Currency = paymentTxn.CurrencyFk.Name,
                CurrencyRate = paymentTxn.CurrencyRate,
                CreationDateTime = paymentTxn.CreationDateTime
            };

            if (paymentTxn.PaymentTransactionLogList.Count > 0)
            {
                foreach (var logItem in paymentTxn.PaymentTransactionLogList)
                {
                    var logDetails = new PaymentTransactionLogDetailsModel
                    {
                        Id = logItem.Id,
                        PaymentTransactionStatusId = logItem.PaymentTransactionStatusId,
                        PaymentTransactionStatus = paymentTxnStatuses.FirstOrDefault(item => item.Id == logItem.PaymentTransactionStatusId)?.Name,
                        LogType = logItem.LogType,
                        ContentType = logItem.ContentType,
                        Content = logItem.Content,
                        Notes = logItem.Notes,
                        CreationDateTime = logItem.CreationDateTime
                    };

                    detailsModel.Logs.Add(logDetails);
                }
            }

            return new SingleResponse<PaymentTransactionDetailsModel>(detailsModel);
        }
    }
}
