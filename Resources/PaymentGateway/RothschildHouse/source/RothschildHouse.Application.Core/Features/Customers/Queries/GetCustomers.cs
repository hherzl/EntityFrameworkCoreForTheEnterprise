using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Customers.Queries
{
    public record CustomerItemModel
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public short? CountryId { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }
    }

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
