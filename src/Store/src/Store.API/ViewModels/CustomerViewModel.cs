using System;
using Store.Core.EntityLayer.Sales;

namespace Store.API.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {

        }

        public CustomerViewModel(Customer entity)
        {
            CustomerID = entity.CustomerID;
            CompanyName = entity.CompanyName;
        }

        public Int32? CustomerID { get; set; }

        public String CompanyName { get; set; }
    }
}
