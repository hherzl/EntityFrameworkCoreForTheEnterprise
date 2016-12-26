using System;

namespace Store.API.ViewModels
{
    public class OrderDetailViewModel
    {
        public Int32? OrderID { get; set; }

        public Int32? ProductID { get; set; }

        public String ProductName { get; set; }

        public Decimal? UnitPrice { get; set; }

        public Int32? Quantity { get; set; }

        public Decimal? Total { get; set; }
    }
}
