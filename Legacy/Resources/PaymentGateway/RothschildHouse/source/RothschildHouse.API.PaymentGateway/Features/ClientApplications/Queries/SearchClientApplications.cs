using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.ClientApplications.Queries
{
#pragma warning disable CS1591
    public class SearchClientApplicationsQueryHandler : IRequestHandler<SearchClientApplicationsQuery, PagedResponse<ClientApplicationItemModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public SearchClientApplicationsQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<ClientApplicationItemModel>> Handle(SearchClientApplicationsQuery request, CancellationToken cancellationToken)
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
