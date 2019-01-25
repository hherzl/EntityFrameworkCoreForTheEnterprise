using System;
using OnLineStore.WebAPI.Clients.Models;

namespace OnLineStore.WebAPI.UnitTests.Mocks
{
    public static class PaymentMocks
    {
        public static PaymentResponse SuccessPayment
            => new PaymentResponse
            {
                ConfirmationID = Guid.NewGuid(),
                PaymentDateTime = DateTime.Now,
                Last4Digits = "1145"
            };
    }
}
