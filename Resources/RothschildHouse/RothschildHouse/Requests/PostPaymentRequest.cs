using System;

namespace RothschildHouse.Requests
{
    public class PostPaymentRequest
    {
        public PostPaymentRequest()
        {
        }

        public string CardHolderName { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string Cvv { get; set; }

        public decimal? Amount { get; set; }
    }
}
