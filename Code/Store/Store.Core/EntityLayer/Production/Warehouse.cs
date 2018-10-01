using System;
using System.Collections.ObjectModel;

namespace Store.Core.EntityLayer.Production
{
    public class Warehouse : IAuditableEntity
    {
        public Warehouse()
        {
        }

        public string WarehouseID { get; set; }

        public string WarehouseName { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public Collection<ProductInventory> ProductInventories { get; set; }
    }
}
