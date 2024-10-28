using System;

namespace RothschildHouse.Responses
{
    /// <summary>
    /// Represents the response for payment transaction
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PaymentResponse"/>
        /// </summary>
        public PaymentResponse()
        {
        }

        /// <summary>
        /// Gets or sets the confirmation ID
        /// </summary>
        public Guid? ConfirmationID { get; set; }

        /// <summary>
        /// Gets or sets the payment datetime
        /// </summary>
        public DateTime? PaymentDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last 4 digits for credit card
        /// </summary>
        public string Last4Digits { get; set; }
    }
}
