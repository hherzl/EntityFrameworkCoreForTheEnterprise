using System;
using System.ComponentModel.DataAnnotations;

namespace RothschildHouse.Requests
{
    public class PostPaymentRequest
    {
        public PostPaymentRequest()
        {
        }

        [Required]
        [StringLength(30)]
        public string CardHolderName { get; set; }

        [Required]
        [StringLength(20)]
        public string CardType { get; set; }

        [Required]
        [StringLength(20)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime? ExpirationDate { get; set; }

        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

        [Required]
        [Range(0.0, 10000.0)]
        public decimal? Amount { get; set; }
    }
}
