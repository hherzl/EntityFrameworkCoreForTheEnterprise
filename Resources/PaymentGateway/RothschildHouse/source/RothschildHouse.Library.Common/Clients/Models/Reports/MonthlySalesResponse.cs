namespace RothschildHouse.Library.Common.Clients.Models.Reports
{
    public record MonthlySalesResponse
    {
        public MonthlySalesResponse()
        {
            Months = new() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Sales = new();
        }

        public List<string> Months { get; set; }
        public List<MonthlySaleItemModel> Sales { get; set; }
    }
}
