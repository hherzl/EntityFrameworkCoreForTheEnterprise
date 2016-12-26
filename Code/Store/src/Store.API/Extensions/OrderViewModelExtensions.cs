using System.Collections.Generic;
using Store.API.ViewModels;
using Store.Core.EntityLayer.Sales;

namespace Store.API.Extensions
{
    public static class OrderViewModelExtensions
    {
        public static Order GetOrder(this OrderViewModel viewModel)
        {
            return new Order
            {
                OrderID = viewModel.OrderID,
                OrderDate = viewModel.OrderDate,
                CustomerID = viewModel.CustomerID,
                EmployeeID = viewModel.EmployeeID,
                ShipperID = viewModel.ShipperID,
                Total = viewModel.Total,
                Comments = viewModel.Comments,
                CreationUser = viewModel.CreationUser,
                CreationDateTime = viewModel.CreationDateTime,
                LastUpdateUser = viewModel.LastUpdateUser,
                LastUpdateDateTime = viewModel.LastUpdateDateTime
            };
        }

        public static IEnumerable<OrderDetail> GetOrderDetails(this OrderViewModel viewModel)
        {
            foreach (var item in viewModel.Details)
            {
                yield return new OrderDetail
                {
                    OrderID = item.OrderID,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
            }
        }
    }
}
