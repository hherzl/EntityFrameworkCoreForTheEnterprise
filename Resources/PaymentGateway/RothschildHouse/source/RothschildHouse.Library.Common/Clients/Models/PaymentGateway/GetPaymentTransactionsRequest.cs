namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetPaymentTransactionsRequest
    {
        public GetPaymentTransactionsRequest()
        {
            PageSize = 25;
            PageNumber = 1;
        }

        public GetPaymentTransactionsRequest(short? paymentTransactionStatusId, Guid? clientApplicationId, DateTime? startDate, DateTime? endDate)
        {
            PageSize = 25;
            PageNumber = 1;
            PaymentTransactionStatusId = paymentTransactionStatusId;
            ClientApplicationId = clientApplicationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public short? PaymentTransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
