using System;

namespace OnlineStore.WebAPI.Clients.Models
{
#pragma warning disable CS1591
    public class PaymentResponse
    {
        public Guid? ConfirmationID { get; set; }

        public DateTime? PaymentDateTime { get; set; }

        public string Last4Digits { get; set; }
    }
#pragma warning restore CS1591
}
