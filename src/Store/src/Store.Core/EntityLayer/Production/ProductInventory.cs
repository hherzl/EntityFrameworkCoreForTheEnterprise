using System;

namespace Store.Core.EntityLayer.Production
{
    public class ProductInventory : IAuditEntity
    {
        public ProductInventory()
        {
        }

        public ProductInventory(Int32? productInventoryID)
        {
            ProductInventoryID = productInventoryID;
        }

        public Int32? ProductInventoryID { get; set; }

        public Int32? ProductID { get; set; }

        public String WarehouseID { get; set; }

        public Int32? Quantity { get; set; }

        public Int32? Stocks { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Product ProductFk { get; set; }
    }
}
