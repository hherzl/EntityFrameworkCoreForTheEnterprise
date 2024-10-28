namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record CustomerItemModel
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public short? CountryId { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }
    }
}
