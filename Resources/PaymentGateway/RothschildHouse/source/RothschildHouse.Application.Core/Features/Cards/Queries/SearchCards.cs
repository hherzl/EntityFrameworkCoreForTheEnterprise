using MediatR;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Cards.Queries
{
    public record CardItemModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
    }

    public  class SearchCardsQuery : IRequest<IPagedResponse<CardItemModel>>
    {
        public SearchCardsQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
