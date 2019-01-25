using System;
using System.Collections.ObjectModel;

namespace OnlineStore.Core.EntityLayer.Sales
{
    public class Shipper : IAuditableEntity
    {
        public Shipper()
        {
        }

        public Shipper(int? shipperID)
        {
            ShipperID = shipperID;
        }

        public int? ShipperID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual Collection<OrderHeader> Orders { get; set; }
    }
}
