namespace RothschildHouse.API.SearchEngine.Models;

public record YearlySaleItemModel
{
    public YearlySaleItemModel()
    {
        Values = new();
    }

    public string Year { get; set; }
    public string Month { get; set; }
    public string ClientApplication { get; set; }
    public List<decimal> Values { get; set; }
}
