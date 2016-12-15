using System;

namespace Store.Core.EntityLayer.Production
{
    public class ProductInventory : IEntity
    {
        public ProductInventory()
        {
        }

        public Int32? ProductInventoryID { get; set; }

        public Int32? ProductID { get; set; }

        public DateTime? EntryDate { get; set; }

        public Int32? Quantity { get; set; }
    }
}
