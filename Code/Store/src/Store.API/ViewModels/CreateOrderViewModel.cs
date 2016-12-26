using System.Collections.Generic;

namespace Store.API.ViewModels
{
    public class CreateOrderViewModel
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public IEnumerable<ShipperViewModel> Shippers { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
