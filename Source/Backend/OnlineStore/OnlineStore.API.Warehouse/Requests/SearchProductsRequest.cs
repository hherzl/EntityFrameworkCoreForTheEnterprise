namespace OnlineStore.API.Warehouse.Requests
{
    public class SearchProductsRequest
    {
        public SearchProductsRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public int? ProductCategoryID { get; set; }
    }
}
