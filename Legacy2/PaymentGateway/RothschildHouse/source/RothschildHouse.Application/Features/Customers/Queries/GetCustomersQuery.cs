using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Customers.Queries;

public class GetCustomersQuery : IRequest<PagedResponse<CustomerItemModel>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public string Name { get; set; }
    public short? CountryId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public bool IsEmpty
        => string.IsNullOrEmpty(Name) && !CountryId.HasValue && string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Email);
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
        var query = _dbContext.GetCustomers(fullName: request.Name, countryId: request.CountryId, phone: request.Phone, email: request.Email)
            .OrderBy(item => item.Name)
            ;

        var list = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(cancellationToken);

        return new PagedResponse<CustomerItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
    }
}
