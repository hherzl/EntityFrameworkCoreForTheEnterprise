using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.Queries
{
    public record PaymentTransactionItemModel
    {
        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CardId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardNumber { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }

    public class GetPaymentTransactionsQuery : IRequest<PagedResponse<PaymentTransactionItemModel>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public short? PaymentTransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsEmpty
            => !PaymentTransactionStatusId.HasValue && !ClientApplicationId.HasValue && !StartDate.HasValue && !EndDate.HasValue;
    }

    public class GetPaymentTransactionsQueryHandler : IRequestHandler<GetPaymentTransactionsQuery, PagedResponse<PaymentTransactionItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetPaymentTransactionsQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<PaymentTransactionItemModel>> Handle(GetPaymentTransactionsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .GetPaymentTransactions(paymentTransactionStatusId: request.PaymentTransactionStatusId, clientApplicationId: request.ClientApplicationId, startDate: request.StartDate, endDate: request.EndDate)
                ;

            if (request.IsEmpty)
                query = query.OrderByDescending(item => item.CreationDateTime);

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
                ;

            return new PagedResponse<PaymentTransactionItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
