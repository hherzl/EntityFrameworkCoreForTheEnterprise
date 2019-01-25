using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Clients.Models
{
    /// <summary>
    /// Represents post payment model
    /// </summary>
    public class PostPaymentRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PostPaymentRequest"/>
        /// </summary>
        public PostPaymentRequest()
        {
        }

        /// <summary>
        /// Card holder name
        /// </summary>
        [Required]
        [StringLength(30)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Issuing network
        /// </summary>
        [Required]
        [StringLength(20)]
        public string IssuingNetwork { get; set; }

        /// <summary>
        /// Card number
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Expiration date
        /// </summary>
        [Required]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Cvv (Card Verification Value)
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [Required]
        [Range(0.0, 10000.0)]
        public decimal? Amount { get; set; }
    }
}
