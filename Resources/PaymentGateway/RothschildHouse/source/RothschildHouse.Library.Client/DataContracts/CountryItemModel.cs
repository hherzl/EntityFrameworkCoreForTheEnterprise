namespace RothschildHouse.Library.Client.DataContracts
{
    public record CountryItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
    }
}
