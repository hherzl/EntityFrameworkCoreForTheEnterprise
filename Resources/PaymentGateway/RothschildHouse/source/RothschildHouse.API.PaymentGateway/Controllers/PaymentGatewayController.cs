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
using RothschildHouse.Application.Core.Features.PaymentTransactions.Commands;
using RothschildHouse.Application.Core.Features.PaymentTransactions.Queries;

namespace RothschildHouse.API.PaymentGateway.Controllers
{
#pragma warning disable CS1591
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
#pragma warning restore CS1591

        /// <summary>
        /// Returns the countries that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The countries.</returns>
        /// <response code="200">Returns the countries</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("search-country")]
        [ProducesResponseType(200, Type = typeof(IListResponse<CountryItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCountriesAsync([FromBody] SearchCountriesQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing country by Id.
        /// </summary>
        /// <param name="id">Country Id</param>
        /// <returns>The country.</returns>
        /// <response code="200">Returns the country</response>
        /// <response code="404">If the resource doesn't exist</response>
        /// <response code="500">If there was an internal error</response>
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

        /// <summary>
        /// Returns the currencies that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The currencies.</returns>
        /// <response code="200">Returns the currencies</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("search-currency")]
        [ProducesResponseType(200, Type = typeof(IListResponse<CurrencyItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCurrenciesAsync([FromBody] SearchCurrenciesQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing currency by Id.
        /// </summary>
        /// <param name="id">Currency Id</param>
        /// <returns>The currency.</returns>
        /// <response code="200">Returns the currency</response>
        /// <response code="404">If the resource doesn't exist</response>
        /// <response code="500">If there was an internal error</response>
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

        /// <summary>
        /// Returns the client applications that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The client applications.</returns>
        /// <response code="200">Returns the client applications</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("search-client-application")]
        [ProducesResponseType(200, Type = typeof(IListResponse<ClientApplicationItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCurrenciesAsync([FromBody] SearchClientApplicationsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing client application by Id.
        /// </summary>
        /// <param name="id">Client application Id</param>
        /// <returns>The client application.</returns>
        /// <response code="200">Returns the client application</response>
        /// <response code="404">If the resource doesn't exist</response>
        /// <response code="500">If there was an internal error</response>
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

        /// <summary>
        /// Returns the cards that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The cards.</returns>
        /// <response code="200">Returns the cards</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("search-card")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CardItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCardsAsync([FromBody] SearchCardsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing card by Id.
        /// </summary>
        /// <param name="id">Card Id</param>
        /// <returns>The card.</returns>
        /// <response code="200">Returns the card</response>
        /// <response code="404">If the resource doesn't exist</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("card/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<CardDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCardAsync(Guid? id)
        {
            var response = await _mediator.Send(new GetCardQuery(id));

            if (response == null)
                return NotFound();

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns the customers that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The customers.</returns>
        /// <response code="200">Returns the customers</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("search-customer")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CustomerItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchCustomersAsync([FromBody] SearchCustomersQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing customer by Id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>The customer.</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the resource doesn't exist</response>
        /// <response code="500">If there was an internal error</response>
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

        /// <summary>
        /// Returns the payment transactions that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The customers.</returns>
        /// <response code="200">Returns the payment transactions</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("payment-txn")]
        [ProducesResponseType(200, Type = typeof(IPagedResponse<PaymentTransactionItemModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchPaymentTransactionsAsync([FromBody] SearchPaymentTransactionsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns the payment transactions that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The payment transactions.</returns>
        /// <response code="200">If resource it was processed  the client applications</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("process-payment-txn")]
        [ProducesResponseType(201, Type = typeof(ProcessPaymentTransactionResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ProcessPaymentTransactionAsync([FromBody] ProcessPaymentTransactionCommand request)
        {
            var response = await _mediator.Send(request);

            return response.ToCreatedResult();
        }
    }
}
