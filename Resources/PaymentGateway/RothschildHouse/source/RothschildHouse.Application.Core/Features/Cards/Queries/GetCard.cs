using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Features.PaymentTransactions.Queries;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Application.Core.Features.Cards.Queries
{
    public record CardDetailsModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string Last4Digits { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }

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

            var paymentTransactions = await _dbContext
                .GetPaymentTransactions(cardId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            paymentTransactions.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

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
                    PaymentTransactions = paymentTransactions
                }
            };
        }
    }
}
