using System;
using System.ComponentModel.DataAnnotations;

namespace OnLineStore.WebAPI.Clients.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostPaymentRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public PostPaymentRequest()
        {
        }

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
        [Required]
        [Range(0.0, 10000.0)]
        public decimal? Amount { get; set; }
    }
}
