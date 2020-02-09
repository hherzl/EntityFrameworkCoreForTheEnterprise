using System.Collections.Generic;
using OnlineStore.API.Common.Clients.Models;
using OnlineStore.Core.Domain.Sales;

namespace OnlineStore.API.Sales.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static OrderHeader GetHeader(this PostOrderRequest request)
            => new OrderHeader
            {
                ID = request.ID,
                CustomerID = request.CustomerID,
                CurrencyID = request.CurrencyID,
                PaymentMethodID = request.PaymentMethodID,
                Comments = request.Comments
            };

        public static IEnumerable<OrderDetail> GetDetails(this PostOrderRequest request)
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
