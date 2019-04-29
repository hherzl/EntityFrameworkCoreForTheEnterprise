using System.Collections.Generic;
using OnlineStore.Core.DomainDrivenDesign.Sales;
using OnlineStore.Core.DomainDrivenDesign.Warehouse;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static Product GetProduct(this PostProductRequest request)
            => new Product
            {
                ProductID = request.ID,
                ProductName = request.ProductName,
                ProductCategoryID = request.ProductCategoryID,
                UnitPrice = request.UnitPrice,
                Description = request.Description
            };

        public static OrderHeader GetOrderHeader(this PostOrderRequest request)
            => new OrderHeader
            {
                OrderHeaderID = request.ID,
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
                    OrderDetailID = item.ID,
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
