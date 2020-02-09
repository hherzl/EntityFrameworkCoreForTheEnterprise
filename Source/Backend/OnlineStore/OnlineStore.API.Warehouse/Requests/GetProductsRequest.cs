namespace OnlineStore.API.Warehouse.Requests
{
#pragma warning disable CS1591
    public class GetProductsRequest
    {
        public GetProductsRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public int? ProductCategoryID { get; set; }
    }
#pragma warning restore CS1591
}
