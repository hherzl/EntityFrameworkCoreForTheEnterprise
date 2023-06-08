using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Customers.Queries
{
    public class GetCustomersQuery : IRequest<PagedResponse<CustomerItemModel>>
    {
        public GetCustomersQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedResponse<CustomerItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCustomersQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<CustomerItemModel>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
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
