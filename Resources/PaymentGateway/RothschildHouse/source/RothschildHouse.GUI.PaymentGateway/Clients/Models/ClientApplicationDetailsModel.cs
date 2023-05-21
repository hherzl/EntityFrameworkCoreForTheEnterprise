namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record ClientApplicationDetailsModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }
}
