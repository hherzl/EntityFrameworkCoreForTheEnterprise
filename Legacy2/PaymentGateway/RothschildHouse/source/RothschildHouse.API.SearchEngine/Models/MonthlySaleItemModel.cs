namespace RothschildHouse.API.SearchEngine.Models;

public record MonthlySaleItemModel
{
    public string Year { get; set; }
    public string Month { get; set; }
    public string ClientApplication { get; set; }
    public decimal? Total { get; set; }
}
