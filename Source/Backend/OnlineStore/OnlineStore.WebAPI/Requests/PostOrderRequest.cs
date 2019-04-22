using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Requests
{
    /// <summary>
    /// Represents the model to post order
    /// </summary>
    public class PostOrderRequest
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the ID for unit tests
        /// </summary>
        public long? ID { get; set; }

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
        /// CVV (Card Verification Value)
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// Currency ID
        /// </summary>
        public string CurrencyID { get; set; }

        /// <summary>
        /// Payment Method ID
        /// </summary>
        public Guid PaymentMethodID { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Order details (lines)
        /// </summary>
        public List<OrderDetailRequest> Details { get; set; }
    }
}
