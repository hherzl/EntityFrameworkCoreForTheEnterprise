using System.Collections.Generic;
using OnLineStore.Core.EntityLayer.Sales;
using OnLineStore.WebAPI.Clients.Models;

namespace OnLineStore.WebAPI.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static OrderHeader GetOrderHeader(this PostOrderRequest request)
            => new OrderHeader
            {
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
