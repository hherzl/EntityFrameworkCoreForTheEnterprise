using System.Collections.Generic;
using Store.Core.EntityLayer.Sales;

namespace Store.API.Requests
{
    public static class Extensions
    {
        public static Order GetOrder(this OrderRequest requestModel)
        {
            return new Order
            {
                OrderDate = requestModel.OrderDate,
                CustomerID = requestModel.CustomerID,
                CurrencyID = requestModel.CurrencyID,
                PaymentMethodID = requestModel.PaymentMethodID,
                Comments = requestModel.Comments,
                CreationUser = requestModel.CreationUser,
                CreationDateTime = requestModel.CreationDateTime
            };
        }

        public static IEnumerable<OrderDetail> GetOrderDetails(this OrderRequest requestModel)
        {
            foreach (var item in requestModel.Details)
            {
                yield return new OrderDetail
                {
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    CreationUser = requestModel.CreationUser,
                    CreationDateTime = requestModel.CreationDateTime
                };
            }
        }
    }
}
