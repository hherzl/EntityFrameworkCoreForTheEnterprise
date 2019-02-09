using System;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway
{
    public static class PaymentResponseMocks
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
