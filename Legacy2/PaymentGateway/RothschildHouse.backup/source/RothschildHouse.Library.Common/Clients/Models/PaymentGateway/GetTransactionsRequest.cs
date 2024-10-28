namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetTransactionsRequest
    {
        public GetTransactionsRequest()
        {
            PageSize = 25;
            PageNumber = 1;
        }

        public GetTransactionsRequest(short? transactionStatusId, Guid? clientApplicationId, DateTime? startDate, DateTime? endDate)
        {
            PageSize = 25;
            PageNumber = 1;
            TransactionStatusId = transactionStatusId;
            ClientApplicationId = clientApplicationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public short? TransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
