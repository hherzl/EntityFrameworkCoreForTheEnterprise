namespace RothschildHouse.API.SearchEngine.Models;

public record YearlySalesResponse
{
    public YearlySalesResponse()
    {
        Months = new() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        Sales = new();
    }

    public List<string> Months { get; set; }
    public List<YearlySaleItemModel> Sales { get; set; }
}
