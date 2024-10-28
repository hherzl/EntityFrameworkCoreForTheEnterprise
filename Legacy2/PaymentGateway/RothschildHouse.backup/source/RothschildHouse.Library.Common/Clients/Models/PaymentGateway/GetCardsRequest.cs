namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCardsRequest
    {
        public GetCardsRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public GetCardsRequest(long? cardTypeId, string issuingNetwork, string cardholderName)
        {
            PageSize = 25;
            PageNumber = 1;
            CardTypeId = cardTypeId;
            IssuingNetwork = issuingNetwork;
            CardholderName = cardholderName;          
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public long? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }        
    }
}
