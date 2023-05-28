namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.Reports
{
    public record MonthlySalesResponse
    {
        public List<string> Months { get; set; }
        public List<MonthlySaleItemModel> Sales { get; set; }
    }
}
