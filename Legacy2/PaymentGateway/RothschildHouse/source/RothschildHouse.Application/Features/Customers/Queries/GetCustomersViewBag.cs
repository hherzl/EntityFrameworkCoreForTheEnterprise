using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Customers.Queries;

public record GetCustomersViewBagRespose : Response
{
    public List<ListItem<short?>> Countries { get; set; }
}

public class GetCustomersViewBagQuery : IRequest<GetCustomersViewBagRespose> { }

public class GetCustomersViewBagQueryHandler : IRequestHandler<GetCustomersViewBagQuery, GetCustomersViewBagRespose>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetCustomersViewBagQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCustomersViewBagRespose> Handle(GetCustomersViewBagQuery request, CancellationToken cancellationToken)
    {
        var countries = await _dbContext.Country.AsNoTracking().ToListAsync(cancellationToken);

        var response = new GetCustomersViewBagRespose
        {
            Countries = countries.Select(item => new ListItem<short?>(item.Id, item.Name)).ToList()
        };

        return response;
    }
}
