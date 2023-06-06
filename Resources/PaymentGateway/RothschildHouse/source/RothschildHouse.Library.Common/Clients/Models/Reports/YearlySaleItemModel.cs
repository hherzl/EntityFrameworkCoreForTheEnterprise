namespace RothschildHouse.Library.Common.Clients.Models.Reports
{
    public record YearlySaleItemModel
    {
        public YearlySaleItemModel()
        {
            Values = new();
        }

        public string Year { get; set; }
        public string Month { get; set; }
        public string ClientApplication { get; set; }
        public List<double> Values { get; set; }
    }
}
