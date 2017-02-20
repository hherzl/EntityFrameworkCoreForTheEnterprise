using System;
using System.Collections.ObjectModel;

namespace Store.Core.EntityLayer.Production
{
    public class ProductCategory : IAuditEntity
    {
        public ProductCategory()
        {
        }

        public ProductCategory(Int32? productCategoryID)
        {
            ProductCategoryID = productCategoryID;
        }

        public Int32? ProductCategoryID { get; set; }

        public String ProductCategoryName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<Product> Products { get; set; }
    }
}
