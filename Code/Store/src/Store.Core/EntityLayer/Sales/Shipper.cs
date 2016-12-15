using System;

namespace Store.Core.EntityLayer.Sales
{
    public class Shipper : IEntity
    {
        public Shipper()
        {
        }

        public Int32? ShipperID { get; set; }

        public String CompanyName { get; set; }

        public String ContactName { get; set; }
    }
}
