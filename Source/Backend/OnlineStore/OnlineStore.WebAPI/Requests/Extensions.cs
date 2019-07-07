using System.Collections.Generic;
using OnlineStore.Core.Domain.Sales;
using OnlineStore.Core.Domain.Warehouse;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static Product GetProduct(this PostProductRequest request)
            => new Product
            {
                ID = request.ID,
                ProductName = request.ProductName,
                ProductCategoryID = request.ProductCategoryID,
                UnitPrice = request.UnitPrice,
                Description = request.Description
            };

        public static OrderHeader GetOrderHeader(this PostOrderRequest request)
            => new OrderHeader
            {
                ID = request.ID,
                CustomerID = request.CustomerID,
                CurrencyID = request.CurrencyID,
                PaymentMethodID = request.PaymentMethodID,
                Comments = request.Comments
            };

        public static IEnumerable<OrderDetail> GetOrderDetails(this PostOrderRequest request)
        {
            foreach (var item in request.Details)
            {
                yield return new OrderDetail
                {
                    ID = item.ID,
                    ProductID = item.ProductID,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                };
            }
        }

        public static PostPaymentRequest GetPostPaymentRequest(this PostOrderRequest request)
            => new PostPaymentRequest
            {
                CardHolderName = request.CardHolderName,
                IssuingNetwork = request.IssuingNetwork,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                Cvv = request.Cvv,
                Amount = request.Total
            };
    }
#pragma warning restore CS1591
}
