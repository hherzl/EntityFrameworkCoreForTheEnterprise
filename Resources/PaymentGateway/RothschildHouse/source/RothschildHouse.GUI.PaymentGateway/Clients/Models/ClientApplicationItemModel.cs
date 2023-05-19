namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record ClientApplicationItemModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
