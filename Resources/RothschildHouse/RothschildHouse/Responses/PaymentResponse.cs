using System;

namespace RothschildHouse.Responses
{
    public class PaymentResponse
    {
        public PaymentResponse()
        {
        }

        public Guid? ConfirmationID { get; set; }

        public DateTime? PaymentDateTime { get; set; }
    }
}
