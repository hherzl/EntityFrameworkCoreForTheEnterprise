using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Cards.Queries
{
#pragma warning disable CS1591
    public class SearchCardsQueryHandler : IRequestHandler<SearchCardsQuery, PagedResponse<CardItemModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public SearchCardsQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<CardItemModel>> Handle(SearchCardsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GetCards();

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
            ;

            list.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

            return new PagedResponse<CardItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
