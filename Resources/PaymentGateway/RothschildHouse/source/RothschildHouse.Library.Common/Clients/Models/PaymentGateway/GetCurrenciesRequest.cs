namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCurrenciesRequest
    {
        public GetCurrenciesRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
