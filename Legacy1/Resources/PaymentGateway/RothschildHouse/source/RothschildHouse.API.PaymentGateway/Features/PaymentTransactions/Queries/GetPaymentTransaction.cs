using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Transactions.Queries
{
#pragma warning disable CS1591
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
        private readonly RothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<PaymentTransactionDetailsModel>> Handle(GetPaymentTransactionQuery request, CancellationToken cancellationToken)
        {
            var paymentTxn = await _dbContext.GetPaymentTransactionAsync(request.Id, tracking: false, cancellationToken: cancellationToken);

            if (paymentTxn == null)
                return SingleResponse<PaymentTransactionDetailsModel>.Empty;

            var paymentTxnStatuses = await _dbContext.VPaymentTransactionStatus.ToListAsync();

            var detailsModel = new PaymentTransactionDetailsModel
            {
                Id = paymentTxn.Id,
                PaymentTransactionStatusId = paymentTxn.PaymentTransactionStatusId,
                PaymentTransactionStatus = paymentTxnStatuses.FirstOrDefault(item => item.Id == paymentTxn.PaymentTransactionStatusId)?.Name,
                ClientApplicationId = paymentTxn.ClientApplicationId,
                ClientApplication = paymentTxn.ClientApplicationFk.Name,
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
