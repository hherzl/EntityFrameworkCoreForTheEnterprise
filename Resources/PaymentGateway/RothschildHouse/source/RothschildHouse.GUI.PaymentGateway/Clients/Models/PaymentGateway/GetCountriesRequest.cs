namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.PaymentGateway
{
    public record GetCountriesRequest
    {
        public GetCountriesRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
