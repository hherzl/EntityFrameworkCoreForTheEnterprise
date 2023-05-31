using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Application.Core.Features.Cards.Queries
{
    public record CardItemModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string Last4Digits { get; set; }
        public string ExpirationDate { get; set; }
    }

    public class GetCardsQuery : IRequest<PagedResponse<CardItemModel>>
    {
        public GetCardsQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, PagedResponse<CardItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCardsQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<CardItemModel>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.GetCards();

            var list = await query
                .Paging(request.PageSize, request.PageNumber)
                .ToListAsync(cancellationToken)
            ;

            list.ForEach(item => item.Last4Digits = item.Last4Digits?[^4..]);

            return new PagedResponse<CardItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
        }
    }
}
