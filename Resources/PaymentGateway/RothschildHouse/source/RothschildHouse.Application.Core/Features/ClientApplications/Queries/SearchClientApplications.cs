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

    public class SearchClientApplicationsQuery : IRequest<IListResponse<ClientApplicationItemModel>>
    {
        public SearchClientApplicationsQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class SearchClientApplicationsQueryHandler : IRequestHandler<SearchClientApplicationsQuery, IListResponse<ClientApplicationItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public SearchClientApplicationsQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IListResponse<ClientApplicationItemModel>> Handle(SearchClientApplicationsQuery request, CancellationToken cancellationToken)
        {
            var clientApplications = await _dbContext.ClientApplication.AsNoTracking().ToListAsync(cancellationToken);

            return new ListResponse<ClientApplicationItemModel>(clientApplications.Select(item => new ClientApplicationItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Url = item.Url
            }));
        }
    }
}
