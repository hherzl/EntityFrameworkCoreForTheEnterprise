namespace RothschildHouse.Library.Common.Clients.Models.Reports
{
    public class MonthlySalesResponse
    {
        public MonthlySalesResponse()
        {
            Sales = new();
        }

        public int Year { get; set; }
        public int Month { get; set; }

        public decimal? Total
            => Sales.Sum(item => item.Total);

        public List<MonthlySaleItemModel> Sales { get; set; }
    }
}
