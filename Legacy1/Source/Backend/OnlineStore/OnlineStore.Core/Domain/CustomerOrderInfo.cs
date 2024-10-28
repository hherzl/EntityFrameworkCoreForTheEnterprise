using System;

namespace OnlineStore.Core.Domain
{
    public class CustomerOrderInfo
    {
        public long? OrderID { get; set; }

        public DateTime? OrderDate { get; set; }

        public short? OrderStatusID { get; set; }

        public string OrderStatusDescription { get; set; }

        public decimal? Total { get; set; }

        public string CurrencyID { get; set; }

        public int? DetailsCount { get; set; }
    }
}
