using System;

namespace Store.Core.EntityLayer.Production
{
    public class ProductCategory : IEntity
    {
        public ProductCategory()
        {
        }

        public Int32? ProductCategoryID { get; set; }

        public String ProductCategoryName { get; set; }
    }
}
