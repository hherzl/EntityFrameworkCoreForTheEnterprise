using System;

namespace Store.Core.EntityLayer.Sales
{
    public class Shipper : IAuditEntity
    {
        public Shipper()
        {
        }

        public Shipper(Int32? shipperID)
        {
            ShipperID = shipperID;
        }

        public Int32? ShipperID { get; set; }

        public String CompanyName { get; set; }

        public String ContactName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
