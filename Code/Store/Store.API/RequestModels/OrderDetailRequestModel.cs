using System;

namespace Store.API.RequestModels
{
    public class OrderDetailRequestModel
    {
        public Int32? OrderID { get; set; }

        public Int32? ProductID { get; set; }

        public String ProductName { get; set; }

        public Decimal? UnitPrice { get; set; }

        public Int32? Quantity { get; set; }

        public Decimal? Total { get; set; }
    }
}
