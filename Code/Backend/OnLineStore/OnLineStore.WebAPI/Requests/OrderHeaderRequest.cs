using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnLineStore.WebAPI.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(20)]
        public string IssuingNetwork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CardNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

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
        public string CurrencyID { get; set; }

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
        public List<OrderDetailRequest> Details { get; set; }
    }
}
