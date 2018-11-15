using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.WebAPI.Requests;
using OnLineStore.WebAPI.Responses;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : ControllerBase
    {
        protected ILogger Logger;
        protected ISalesService SalesService;

        public SalesController(ILogger<SalesController> logger, ISalesService salesService)
        {
            Logger = logger;
            SalesService = salesService;
        }
#pragma warning restore CS1591

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="currencyID"></param>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <param name="orderStatusID"></param>
        /// <param name="paymentMethodID"></param>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        [HttpGet("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOrdersAsync(int? pageSize = 50, int? pageNumber = 1, short? currencyID = null, int? customerID = null, int? employeeID = null, short? orderStatusID = null, Guid? paymentMethodID = null, int? shipperID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesService.GetOrdersAsync((int)pageSize, (int)pageNumber, currencyID, customerID, employeeID, orderStatusID, paymentMethodID, shipperID);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOrderAsync(long id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            // Get response from business logic
            var response = await SalesService.GetOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateOrderRequest")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            // Get response from business logic
            var response = await SalesService.GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest value)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

            // Get response from business logic
            var response = await SalesService.CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("CloneOrder/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesService.CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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
