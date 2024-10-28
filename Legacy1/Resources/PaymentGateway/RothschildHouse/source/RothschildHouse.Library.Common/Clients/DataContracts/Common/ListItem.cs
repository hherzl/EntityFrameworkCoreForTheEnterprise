namespace RothschildHouse.Library.Common.Clients.DataContracts.Common
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
