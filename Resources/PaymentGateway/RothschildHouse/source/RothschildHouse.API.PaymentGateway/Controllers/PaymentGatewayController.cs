using MediatR;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.PaymentGateway.Models;
using RothschildHouse.Library.Client.DataContracts;

namespace RothschildHouse.API.PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PaymentGatewayController
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly IMediator _mediator;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("search-country")]
        public async Task<IActionResult> SearchCountriesAsync([FromBody] SearchCountriesQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }
    }
}
