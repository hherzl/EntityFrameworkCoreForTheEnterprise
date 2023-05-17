using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Transactions.Queries
{
#pragma warning disable CS1591
    public class SearchPaymentTransactionsQueryHandler : IRequestHandler<SearchPaymentTransactionsQuery, PagedResponse<PaymentTransactionItemModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public SearchPaymentTransactionsQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<PaymentTransactionItemModel>> Handle(SearchPaymentTransactionsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .GetPaymentTransactions(paymentTransactionStatusId: request.PaymentTransactionStatusId, clientApplicationId: request.ClientApplicationId, startDate: request.StartDate, endDate: request.EndDate)
                ;

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
                ;

            return new PagedResponse<PaymentTransactionItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
