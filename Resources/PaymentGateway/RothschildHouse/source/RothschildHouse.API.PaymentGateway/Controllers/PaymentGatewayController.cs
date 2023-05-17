using Microsoft.AspNetCore.Mvc;

namespace RothschildHouse.API.PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PaymentGatewayController
    {
        private readonly ILogger<PaymentGatewayController> _logger;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger)
        {
            _logger = logger;
        }
    }
}
