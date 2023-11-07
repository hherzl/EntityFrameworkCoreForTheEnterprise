namespace RothschildHouse.Library.Common.Clients.Models.Reports
{
    public record MonthlySaleItemModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string ClientApplication { get; set; }
        public decimal? Total { get; set; }
    }
}
