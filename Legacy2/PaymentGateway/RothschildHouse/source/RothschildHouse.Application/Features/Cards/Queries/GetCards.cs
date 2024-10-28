using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Cards.Queries;

public class GetCardsQuery : IRequest<PagedResponse<CardItemModel>>
{
    public GetCardsQuery()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public long? CardTypeId { get; set; }
    public string CardholderName { get; set; }
    public string IssuingNetwork { get; set; }

    public bool? IsEmpty
        => !CardTypeId.HasValue && string.IsNullOrEmpty(CardholderName) && string.IsNullOrEmpty(IssuingNetwork);
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
        var query = _dbContext.GetCards(cardTypeId: request.CardTypeId, issuingNetwork: request.IssuingNetwork, cardholderName: request.CardholderName);

        var list = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(cancellationToken);

        list.ForEach(item => item.Last4Digits = item.Last4Digits?[^4..]);

        return new PagedResponse<CardItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
    }
}
