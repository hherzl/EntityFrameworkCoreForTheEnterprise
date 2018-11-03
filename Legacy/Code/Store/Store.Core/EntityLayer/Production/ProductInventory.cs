using System;

namespace Store.Core.EntityLayer.Production
{
    public class ProductInventory : IAuditableEntity
    {
        public ProductInventory()
        {
        }

        public ProductInventory(int? productInventoryID)
        {
            ProductInventoryID = productInventoryID;
        }

        public int? ProductInventoryID { get; set; }

        public int? ProductID { get; set; }

        public string WarehouseID { get; set; }

        public int? Quantity { get; set; }

        public int? Stocks { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public Product ProductFk { get; set; }

        public Warehouse WarehouseFk { get; set; }
    }
}
