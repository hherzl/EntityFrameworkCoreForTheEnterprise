﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStore.API.Common.Clients.Contracts;
using OnlineStore.API.Common.Clients.Models;
using OnlineStore.API.Common.Controllers;
using OnlineStore.API.Common.Filters;
using OnlineStore.API.Common.Responses;
using OnlineStore.API.Sales.Requests;
using OnlineStore.API.Sales.Security;
using OnlineStore.Core.Business.Contracts;

namespace OnlineStore.API.Sales.Controllers
{
#pragma warning disable CS1591
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalesController : OnlineStoreController
    {
        readonly ILogger Logger;
        readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        readonly IRothschildHousePaymentClient RothschildHousePaymentClient;
        readonly ISalesService SalesService;

        public SalesController(ILogger<SalesController> logger, IRothschildHouseIdentityClient rothschildHouseIdentityClient, IRothschildHousePaymentClient rothschildHousePaymentClient, ISalesService salesService)
            : base()
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
        /// <param name="request">Search request</param>
        /// <returns>A sequence of orders</returns>
        /// <response code="200">Returns a list of orders</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]

        public async Task<IActionResult> GetOrdersAsync([FromQuery]GetOrdersRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesService
                .GetOrdersForCustomerAsync((int)request.PageSize, (int)request.PageNumber, (int)request.CustomerID, request.OrderStatusID);

            // Return as http response
            return response.ToHttpResult();
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
        [HttpGet("order/{id}")]
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
            return response.ToHttpResult();
        }

        /// <summary>
        /// Retrieves the request model to create a new order
        /// </summary>
        /// <returns>A model that represents the request to create a new order</returns>
        /// <response code="200">Returns the model to create a new order</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("order-model")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetPostOrderModelAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetPostOrderModelAsync));

            // Get response from business logic
            var response = await SalesService.GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResult();
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
        [HttpPost("order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> PostOrderAsync([FromBody]PostOrderRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(PostOrderAsync));

            var token = await RothschildHouseIdentityClient
                .GetRothschildHouseTokenAsync();

            if (token.IsError)
                return Unauthorized();

            var paymentRequest = request.GetPostPaymentRequest();

            var paymentHttpResponse = await RothschildHousePaymentClient
                .PostPaymentAsync(token, paymentRequest);

            if (!paymentHttpResponse.IsSuccessStatusCode)
                return BadRequest();

            var paymentResponse = await paymentHttpResponse
                .GetPaymentResponseAsync();

            var entity = request.GetHeader();

            entity.CreationUser = UserInfo.UserName;

            // Get response from business logic
            var response = await SalesService
                .CreateOrderAsync(entity, request.GetDetails().ToArray());

            // Return as http response
            return response.ToHttpResult();
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
        [HttpGet("order/{id}/clone")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesService.CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResult();
        }

        /// <summary>
        /// Cancels an existing order
        /// </summary>
        /// <param name="id">ID for order</param>
        /// <returns>A success response if order is cancelled</returns>
        /// <response code="200">If order was cancelled successfully</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpDelete("order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(DeleteOrderAsync));

            // Get response from business logic
            var response = await SalesService.CancelOrderAsync(id);

            // Return as http response
            return response.ToHttpResult();
        }
    }
}
