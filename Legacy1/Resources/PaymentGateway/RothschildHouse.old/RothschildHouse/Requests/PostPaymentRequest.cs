using System;
using System.ComponentModel.DataAnnotations;

namespace RothschildHouse.Requests
{
    /// <summary>
    /// Represents the model for payment request
    /// </summary>
    public class PostPaymentRequest
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PostPaymentRequest"/>
        /// </summary>
        public PostPaymentRequest()
        {
        }

        /// <summary>
        /// Gets or sets the card holder's name
        /// </summary>
        [Required]
        [StringLength(30)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the issuing network
        /// </summary>
        [Required]
        [StringLength(20)]
        public string IssuingNetwork { get; set; }

        /// <summary>
        /// Gets or sets the card number
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiration date
        /// </summary>
        [Required]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the CVV (Card Verification Value)
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

        /// <summary>
        /// Gets or sets the amount
        /// </summary>
        [Required]
        [Range(0.0, 10000.0)]
        public decimal? Amount { get; set; }
    }
}
