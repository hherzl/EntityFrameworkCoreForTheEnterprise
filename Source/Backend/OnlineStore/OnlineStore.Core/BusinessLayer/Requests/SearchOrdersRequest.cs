using System;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public class SearchOrdersRequest
    {
        public SearchOrdersRequest()
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
}
