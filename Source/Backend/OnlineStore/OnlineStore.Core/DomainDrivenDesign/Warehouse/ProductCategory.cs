using System;
using System.Collections.ObjectModel;

namespace OnlineStore.Core.Domain.Warehouse
{
    public class ProductCategory : IAuditableEntity
    {
        public ProductCategory()
        {
        }

        public ProductCategory(int? id)
        {
            ID = id;
        }

        public int? ID { get; set; }

        public string ProductCategoryName { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual Collection<Product> Products { get; set; }
    }
}
