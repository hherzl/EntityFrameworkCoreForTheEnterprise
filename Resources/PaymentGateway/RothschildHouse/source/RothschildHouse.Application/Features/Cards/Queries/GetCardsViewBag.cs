using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Cards.Queries;

public record GetCardsViewBagResponse : Response
{
    public List<ListItem<long?>> CardTypes { get; set; }
}

public class GetCardsViewBagQuery : IRequest<GetCardsViewBagResponse> { }

public class GetCardsViewBagQueryHandler : IRequestHandler<GetCardsViewBagQuery, GetCardsViewBagResponse>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetCardsViewBagQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCardsViewBagResponse> Handle(GetCardsViewBagQuery request, CancellationToken cancellationToken)
    {
        var cardTypes = await _dbContext.VCardType.AsNoTracking().ToListAsync(cancellationToken);

        return new GetCardsViewBagResponse
        {
            CardTypes = cardTypes.Select(item => new ListItem<long?>(item.Id, item.Name)).ToList()
        };
    }
}
