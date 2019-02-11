using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.WebAPI.Clients.Contracts;
using OnlineStore.WebAPI.Clients.Models;
using OnlineStore.WebAPI.Filters;
using OnlineStore.WebAPI.Requests;
using OnlineStore.WebAPI.Responses;

namespace OnlineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : OnlineStoreController
    {
        protected readonly ILogger Logger;
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly IRothschildHousePaymentClient RothschildHousePaymentClient;
        protected readonly ISalesService SalesService;

        public SalesController(
            ILogger<SalesController> logger,
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
            IRothschildHousePaymentClient rothschildHousePaymentClient,
            ISalesService salesService
        ) : base()
        {
            Logger = logger;
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            RothschildHousePaymentClient = rothschildHousePaymentClient;
            SalesService = salesService;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Retrieves the orders placed by customers
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="orderStatusID">Order status</param>
        /// <param name="customerID">Customer</param>
        /// <param name="employeeID">Employee</param>
        /// <param name="shipperID">Shipper</param>
        /// <param name="currencyID">Currency</param>
        /// <param name="paymentMethodID">Payment method</param>
        /// <returns>A sequence of orders</returns>
        /// <response code="200">Returns a list of orders</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> GetOrdersAsync(int? pageSize = 50, int? pageNumber = 1, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, string currencyID = null, Guid? paymentMethodID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesService.GetOrdersAsync((int)pageSize, (int)pageNumber, orderStatusID, customerID, employeeID, shipperID, currencyID, paymentMethodID);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Retrieves an existing order by id
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>An existing order</returns>
        /// <response code="200">If id exists</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> GetOrderAsync(long id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            // Get response from business logic
            var response = await SalesService.GetOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Retrieves the request model to create a new order
        /// </summary>
        /// <returns>A model that represents the request to create a new order</returns>
        /// <response code="200">Returns the model to create a new order</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("CreateOrderRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            // Get response from business logic
            var response = await SalesService.GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>A result that contains the order ID generated by API</returns>
        /// <response code="200">If order was created successfully</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> PostOrderAsync([FromBody]PostOrderRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(PostOrderAsync));

            var token = await RothschildHouseIdentityClient.GetRothschildHouseTokenAsync();

            if (token.IsError)
                return Unauthorized();

            var paymentRequest = request.GetPostPaymentRequest();

            var paymentHttpResponse = await RothschildHousePaymentClient.PostPaymentAsync(token, paymentRequest);

            if (!paymentHttpResponse.IsSuccessStatusCode)
                return BadRequest();

            var paymentResponse = await paymentHttpResponse.GetPaymentResponseAsync();

            var entity = request.GetOrderHeader();

            entity.CreationUser = UserInfo.UserName;

            // Get response from business logic
            var response = await SalesService.CreateOrderAsync(entity, request.GetOrderDetails().ToArray());

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new order model from existing order
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>A model for a new order</returns>
        /// <response code="200">If order was cloned successfully</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("CloneOrder/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesService.CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Deletes an existing order
        /// </summary>
        /// <param name="id">ID for order</param>
        /// <returns>A success response if order is deleted</returns>
        /// <response code="200">If order was deleted successfully</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpDelete("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(DeleteOrderAsync));

            // Get response from business logic
            var response = await SalesService.RemoveOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
