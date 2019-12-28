namespace OnlineStore.API.Warehouse.Requests
{
    public class GetProductInventoryRequest
    {
        public int? ProductID { get; set; }

        public string LocationID { get; set; }
    }
}
