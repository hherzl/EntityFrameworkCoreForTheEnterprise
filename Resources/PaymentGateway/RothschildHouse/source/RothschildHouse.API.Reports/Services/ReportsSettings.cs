namespace RothschildHouse.API.Reports.Services
{
    public record ReportsSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string CollectionName { get; set; }
    }
}
