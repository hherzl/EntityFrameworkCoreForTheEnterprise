using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.PaymentGateway.Extensions;
using RothschildHouse.API.PaymentGateway.Features.Cards.Queries;
using RothschildHouse.API.PaymentGateway.Features.ClientApplications.Queries;
using RothschildHouse.API.PaymentGateway.Features.Customers.Queries;
using RothschildHouse.API.PaymentGateway.Features.PaymentTransactions.Queries;
using RothschildHouse.API.PaymentGateway.Features.Transactions.Queries;
using RothschildHouse.API.PaymentGateway.Filters;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1")]
    [TypeFilter(typeof(RothschildHouseExceptionFilter))]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;

        public PaymentGatewayController(IWebHostEnvironment webHostEnvironment, IMediator mediator)
        {
            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
        }

        protected string ClientId
            => string.Concat(Request.Headers["ClientId"]);

        protected string ClientSecret
            => string.Concat(Request.Headers["ClientSecret"]);

        protected bool AreHeaderCredentialsNullOrEmpty()
            => string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret);

        [HttpGet("monitor")]
        public IActionResult GetMonitor()
            => Ok(new { Environment = _webHostEnvironment.EnvironmentName, DateTime = DateTime.Now });

#pragma warning restore CS1591

        /// <summary>
        /// Returns the client applications that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The client applications.</returns>
        /// <response code="200">Returns the client applications</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("search-client-application")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<ClientApplicationItemModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchClientApplicationsAsync([FromQuery] SearchClientApplicationsQuery request)
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
        [HttpGet("client-application/{id:guid}")]
        [ProducesResponseType(200, Type = typeof(SingleResponse<ClientApplicationDetailsModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClientApplicationAsync(Guid id)
        {
            var response = await _mediator.Send(new GetClientApplicationQuery(id));

            if (response.Model == null)
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
        [HttpGet("search-card")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CardItemModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchCardsAsync([FromQuery] SearchCardsQuery request)
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
        [HttpGet("card/{id:guid}")]
        [ProducesResponseType(200, Type = typeof(SingleResponse<CardDetailsModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCardAsync(Guid id)
        {
            var response = await _mediator.Send(new GetCardQuery(id));

            if (response.Model == null)
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
        [HttpGet("search-customer")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<CustomerItemModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchCustomersAsync([FromQuery] SearchCustomersQuery request)
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
        [HttpGet("customer/{id:guid}")]
        [ProducesResponseType(200, Type = typeof(SingleResponse<CustomerDetailsModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomerAsync(Guid id)
        {
            var response = await _mediator.Send(new GetCustomerQuery { Id = id });

            if (response.Model == null)
                return NotFound();

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing customer by U-Commerce Id.
        /// </summary>
        /// <param name="id">Client application Id</param>
        /// <returns>The client application.</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the resource doesn't exist</response>
        [HttpGet("customer-by-alienguid/{id:guid}")]
        [ProducesResponseType(200, Type = typeof(SingleResponse<CustomerDetailsModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomerByAlienGuidAsync(Guid id)
        {
            var response = await _mediator.Send(new GetCustomerQuery { AlienGuid = id });

            if (response.Model == null)
                return NotFound();

            return response.ToOkResult();
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">Customer information</param>
        /// <returns>The client application.</returns>
        /// <response code="201">If resource it was created successfully</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("customer")]
        [ProducesResponseType(201, Type = typeof(CreatedResponse<Guid?>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerCommand request)
        {
            var response = await _mediator.Send(request);

            return response.ToCreatedResult();
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="request">Customer information</param>
        /// <returns>The client application.</returns>
        /// <response code="200">If resource it was updated successfully</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="404">If the resource doesn't exist</response>
        [HttpPut("customer")]
        [ProducesResponseType(200, Type = typeof(Response))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerCommand request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns the payment transactions that match with the specified search criteria.
        /// </summary>
        /// <returns>The payment transactions.</returns>
        /// <response code="200">Returns the client applications</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("search-payment-txn-viewbag")]
        [ProducesResponseType(200, Type = typeof(SearchPaymentTransactionsViewBagRespose))]
        [ProducesResponseType(400)]
        //[Authorize(Policy = Policies.SearchPaymentTransactions)]
        public async Task<IActionResult> SearchPaymentTransactionsViewBagAsync()
        {
            var response = await _mediator.Send(new SearchPaymentTransactionsViewBagQuery());

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns the payment transactions that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The payment transactions.</returns>
        /// <response code="200">Returns the client applications</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("search-payment-txn")]
        [ProducesResponseType(200, Type = typeof(PagedResponse<PaymentTransactionItemModel>))]
        [ProducesResponseType(400)]
        //[Authorize(Policy = Policies.SearchPaymentTransactions)]
        public async Task<IActionResult> SearchPaymentTransactionsAsync([FromBody] SearchPaymentTransactionsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns an existing payment transaction by Id.
        /// </summary>
        /// <param name="id">Payment transaction Id</param>
        /// <returns>The payment transaction.</returns>
        /// <response code="200">Returns the payment transaction</response>
        /// <response code="404">If the resource doesn't exist</response>
        [HttpGet("payment-txn/{id:long}")]
        [ProducesResponseType(200, Type = typeof(SingleResponse<PaymentTransactionDetailsModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPaymentTransactionAsync(long? id)
        {
            var response = await _mediator.Send(new GetPaymentTransactionQuery(id));

            if (response.Model == null)
                return NotFound();

            return response.ToOkResult();
        }

        /// <summary>
        /// Returns the payment transactions that match with the specified search criteria.
        /// </summary>
        /// <param name="request">Search parameters</param>
        /// <returns>The payment transactions.</returns>
        /// <response code="200">If resource it was processed  the client applications</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost("process-payment-txn")]
        [ProducesResponseType(201, Type = typeof(ProcessPaymentTransactionResponse))]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(DbTransactionFilter))]
        public async Task<IActionResult> ProcessPaymentTransactionAsync([FromBody] ProcessPaymentTransactionCommand request)
        {
            var response = await _mediator.Send(request);

            return response.ToCreatedResult();
        }
    }
}
