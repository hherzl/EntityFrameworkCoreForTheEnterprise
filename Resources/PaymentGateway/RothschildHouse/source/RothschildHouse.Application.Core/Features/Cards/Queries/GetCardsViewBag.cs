using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Cards.Queries
{
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

            var response = new GetCardsViewBagResponse
            {
                CardTypes = cardTypes.Select(item => new ListItem<long?>(item.Id, item.Name)).ToList()
            };

            return response;
        }        
    }
}
