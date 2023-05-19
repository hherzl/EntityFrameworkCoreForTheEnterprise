using MediatR;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.PaymentGateway.Models;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Features.Cards.Queries;
using RothschildHouse.Application.Core.Features.ClientApplications.Queries;
using RothschildHouse.Application.Core.Features.Countries.Queries;
using RothschildHouse.Application.Core.Features.Currencies.Queries;
using RothschildHouse.Application.Core.Features.Customers.Queries;

namespace RothschildHouse.API.PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly IMediator _mediator;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("search-country")]
        [ProducesResponseType(200, Type = typeof(IListResponse<CountryItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCountriesAsync([FromBody] SearchCountriesQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        [HttpGet("country/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<CountryDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCountriesAsync(short? id)
        {
            var response = await _mediator.Send(new GetCountryQuery(id));

            if (response == null)
                return NotFound();

            return response.ToOkResult();
        }

        [HttpPost("search-currency")]
        [ProducesResponseType(200, Type = typeof(IListResponse<CurrencyItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCurrenciesAsync([FromBody] SearchCurrenciesQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        [HttpGet("currency/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<CurrencyDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCurrencyAsync(short? id)
        {
            var response = await _mediator.Send(new GetCurrencyQuery(id));

            if (response == null)
                return NotFound();

            return response.ToOkResult();
        }

        [HttpPost("search-client-application")]
        [ProducesResponseType(200, Type = typeof(IListResponse<ClientApplicationItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCurrenciesAsync([FromBody] SearchClientApplicationsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        [HttpGet("client-application/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<ClientApplicationDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetClientApplicationAsync(Guid? id)
        {
            var response = await _mediator.Send(new GetClientApplicationQuery(id));

            if (response == null)
                return NotFound();

            return response.ToOkResult();
        }

        [HttpPost("search-customer")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CustomerItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCustomersAsync([FromBody] SearchCustomersQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        [HttpGet("customer/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<CustomerDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerAsync(Guid? id)
        {
            var response = await _mediator.Send(new GetCustomerQuery(id));

            if (response == null)
                return NotFound();

            return response.ToOkResult();
        }

        [HttpGet("search-card")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CardItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCardsAsync([FromBody] SearchCardsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }
    }
}
