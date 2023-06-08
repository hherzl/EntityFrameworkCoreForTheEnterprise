namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record ClientApplicationDetailsModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<TransactionItemModel> Transactions { get; set; }
    }
}
