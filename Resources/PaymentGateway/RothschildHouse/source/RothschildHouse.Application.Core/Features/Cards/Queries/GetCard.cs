using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Cards.Queries
{
    public class GetCardQuery : IRequest<SingleResponse<CardDetailsModel>>
    {
        public GetCardQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetPaymentTransactionQueryHandler : IRequestHandler<GetCardQuery, SingleResponse<CardDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CardDetailsModel>> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCardAsync(request.Id, true, true, cancellationToken);

            if (entity == null)
                return null;

            var transactions = await _dbContext
                .GetTransactions(cardId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            transactions.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

            return new SingleResponse<CardDetailsModel>
            {
                Model = new()
                {
                    Id = entity.Id,
                    CardTypeId = entity.CardTypeId,
                    CardType = entity.CardTypeId == (short)CardType.Debit ? "Debit" : "Credit",
                    IssuingNetwork = entity.IssuingNetwork,
                    CardholderName = entity.CardholderName,
                    Last4Digits = entity.CardNumber?[^4..],
                    ExpirationDate = entity.ExpirationDate,
                    Cvv = "****",
                    Transactions = transactions
                }
            };
        }
    }
}
