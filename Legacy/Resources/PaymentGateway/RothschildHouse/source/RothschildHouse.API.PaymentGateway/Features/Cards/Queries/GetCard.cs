using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Domain.Enums;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Cards.Queries
{
#pragma warning disable CS1591
    public class GetCardQuery : IRequest<SingleResponse<CardDetailsModel>>
    {
        public GetCardQuery()
        {
        }

        public GetCardQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetPaymentTransactionQueryHandler : IRequestHandler<GetCardQuery, SingleResponse<CardDetailsModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CardDetailsModel>> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCardAsync(request.Id, true, true, cancellationToken);

            if (entity == null)
                return SingleResponse<CardDetailsModel>.Empty;

            var paymentTransactions = await _dbContext
                .GetPaymentTransactions(cardId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            return new SingleResponse<CardDetailsModel>
            {
                Model = new()
                {
                    Id = entity.Id,
                    CardTypeId = entity.CardTypeId,
                    CardType = entity.CardTypeId == (short)CardType.Debit ? "Debit" : "Credit",
                    IssuingNetwork = entity.IssuingNetwork,
                    CardholderName = entity.CardholderName,
                    CardNumber = entity.CardNumber?[^4..],
                    ExpirationDate = entity.ExpirationDate,
                    Cvv = entity.Cvv,
                    PaymentTransactions = paymentTransactions
                }
            };
        }
    }
}
