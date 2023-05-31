namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.Common
{
    public record ListItem<TKey>
    {
        public ListItem()
        {
        }

        public ListItem(TKey id, string name)
        {
            Id = id;
            Name = name;
        }

        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}
