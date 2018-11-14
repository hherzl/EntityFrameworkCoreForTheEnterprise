using System;
using System.Collections.Generic;

namespace OnLineStore.WebAPI.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int? OrderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? CurrencyID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid PaymentMethodID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreationUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreationDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderDetailRequest> Details { get; set; }
    }
}
