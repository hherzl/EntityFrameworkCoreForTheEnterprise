using System;

namespace OnlineStore.API.Sales.Requests
{
#pragma warning disable CS1591
    public class GetOrdersRequest
    {
        public GetOrdersRequest()
        {
            PageSize = 50;
            PageNumber = 1;
        }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public short? OrderStatusID { get; set; }

        public int? CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public int? ShipperID { get; set; }

        public string CurrencyID { get; set; }

        public Guid? PaymentMethodID { get; set; }
    }
#pragma warning restore CS1591
}
