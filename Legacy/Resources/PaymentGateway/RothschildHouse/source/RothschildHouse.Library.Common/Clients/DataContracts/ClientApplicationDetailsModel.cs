namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record ClientApplicationDetailsModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }
}
