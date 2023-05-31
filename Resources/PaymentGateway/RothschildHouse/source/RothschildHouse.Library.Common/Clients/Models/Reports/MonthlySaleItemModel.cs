namespace RothschildHouse.Library.Common.Clients.Models.Reports
{
    public record MonthlySaleItemModel
    {
        public MonthlySaleItemModel()
        {
            Values = new();
        }

        public string Year { get; set; }
        public string Month { get; set; }
        public string ClientApplication { get; set; }
        public List<double> Values { get; set; }
    }
}
