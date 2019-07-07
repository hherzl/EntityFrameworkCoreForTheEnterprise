using System;

namespace OnlineStore.WebAPI.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchOrdersRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public SearchOrdersRequest()
        {
            PageSize = 50;
            PageNumber = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? OrderStatusID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? EmployeeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ShipperID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrencyID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? PaymentMethodID { get; set; }
    }
}
