namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetClientApplicationsRequest
    {
        public GetClientApplicationsRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
