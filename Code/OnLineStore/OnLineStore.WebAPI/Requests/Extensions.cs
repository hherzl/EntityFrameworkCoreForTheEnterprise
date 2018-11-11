using System.Collections.Generic;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.WebAPI.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static Order GetOrder(this OrderRequest request)
        {
            return new Order
            {
                OrderDate = request.OrderDate,
                CustomerID = request.CustomerID,
                CurrencyID = request.CurrencyID,
                PaymentMethodID = request.PaymentMethodID,
                Comments = request.Comments,
                CreationUser = request.CreationUser,
                CreationDateTime = request.CreationDateTime
            };
        }

        public static IEnumerable<OrderDetail> GetOrderDetails(this OrderRequest request)
        {
            foreach (var item in request.Details)
            {
                yield return new OrderDetail
                {
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    CreationUser = request.CreationUser,
                    CreationDateTime = request.CreationDateTime
                };
            }
        }
    }
#pragma warning restore CS1591
}
