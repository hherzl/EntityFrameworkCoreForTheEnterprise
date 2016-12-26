using System;
using Store.Core.EntityLayer.Production;

namespace Store.API.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product entity)
        {
            ProductID = entity.ProductID;
            ProductName = entity.ProductName;
        }

        public Int32? ProductID { get; set; }

        public String ProductName { get; set; }
    }
}
