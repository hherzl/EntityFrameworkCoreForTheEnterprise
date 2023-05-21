using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.ClientApplications.Queries
{
    public record ClientApplicationItemModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class GetClientApplicationsQuery : IRequest<IListResponse<ClientApplicationItemModel>>
    {
        public GetClientApplicationsQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetClientApplicationsQueryHandler : IRequestHandler<GetClientApplicationsQuery, IListResponse<ClientApplicationItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetClientApplicationsQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IListResponse<ClientApplicationItemModel>> Handle(GetClientApplicationsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .ClientApplication
                .AsNoTracking()
                ;

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
                ;

            var model = list
                .Select(item => new ClientApplicationItemModel { Id = item.Id, Name = item.Name, Url = item.Url })
                .ToList()
                ;

            return new PagedResponse<ClientApplicationItemModel>(model, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
