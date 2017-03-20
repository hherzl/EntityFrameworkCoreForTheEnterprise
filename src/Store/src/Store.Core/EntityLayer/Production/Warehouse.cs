using System;
using System.Collections.ObjectModel;

namespace Store.Core.EntityLayer.Production
{
    public class Warehouse : IAuditEntity
    {
        public Warehouse()
        {
        }

        public String WarehouseID { get; set; }

        public String WarehouseName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public Collection<ProductInventory> ProductInventories { get; set; }
    }
}
