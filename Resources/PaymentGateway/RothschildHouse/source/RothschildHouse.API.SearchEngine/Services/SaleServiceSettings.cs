namespace RothschildHouse.API.SearchEngine.Services;

public record SaleServiceSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string CollectionName { get; set; }
}
