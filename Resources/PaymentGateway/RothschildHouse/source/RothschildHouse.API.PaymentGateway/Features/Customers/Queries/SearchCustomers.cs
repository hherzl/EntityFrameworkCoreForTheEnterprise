using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Customers.Queries
{
#pragma warning disable CS1591
    public class SearchCustomersQueryHandler : IRequestHandler<SearchCustomersQuery, PagedResponse<CustomerItemModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public SearchCustomersQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<CustomerItemModel>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GetCustomers();

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
                ;

            return new PagedResponse<CustomerItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
