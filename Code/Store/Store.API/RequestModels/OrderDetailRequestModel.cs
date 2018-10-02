namespace Store.API.RequestModels
{
    public class OrderDetailRequestModel
    {
        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public decimal? Total { get; set; }
    }
}
