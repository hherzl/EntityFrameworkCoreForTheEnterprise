namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record GetPaymentTransactionsRequest
    {
        public GetPaymentTransactionsRequest()
        {
            PageSize = 25;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public short? PaymentTransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
