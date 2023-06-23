using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
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
            => new GetCardsViewBagResponse
            {
                CardTypes = await _dbContext.VCardType.AsNoTracking().ToListAsync(cancellationToken)
            };        
    }
}
