using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.ClientApplications.Queries;

public record ClientApplicationItemModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}

public class GetClientApplicationsQuery : IRequest<ListResponse<ClientApplicationItemModel>>
{
    public GetClientApplicationsQuery()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class GetClientApplicationsQueryHandler : IRequestHandler<GetClientApplicationsQuery, ListResponse<ClientApplicationItemModel>>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetClientApplicationsQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListResponse<ClientApplicationItemModel>> Handle(GetClientApplicationsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.ClientApplication.AsNoTracking().Paging(request.PageSize, request.PageNumber);

        var model = await query.ToListAsync(cancellationToken);

        return new PagedResponse<ClientApplicationItemModel>(model.Select(item => new ClientApplicationItemModel
        {
            Id = item.Id,
            Name = item.Name,
            Url = item.Url
        }), request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
    }
}
