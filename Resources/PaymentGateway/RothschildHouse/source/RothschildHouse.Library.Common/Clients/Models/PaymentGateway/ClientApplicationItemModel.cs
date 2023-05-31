namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record ClientApplicationItemModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
