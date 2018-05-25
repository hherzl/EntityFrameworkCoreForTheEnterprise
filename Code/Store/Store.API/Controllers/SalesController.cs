using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.RequestModels;
using Store.API.Responses;
using Store.Core.BusinessLayer.Contracts;

namespace Store.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SalesController : Controller
    {
        protected ILogger Logger;
        protected ISalesService SalesService;

        public SalesController(ILogger<SalesController> logger, ISalesService salesService)
        {
            Logger = logger;
            SalesService = salesService;
        }

        protected override void Dispose(bool disposing)
        {
            SalesService?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet("Order")]
        public async Task<IActionResult> GetOrdersAsync(int? pageSize = 10, int? pageNumber = 1, short? currencyID = null, int? customerID = null, int? employeeID = null, short? orderStatusID = null, Guid? paymentMethodID = null, int? shipperID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesService.GetOrdersAsync((int)pageSize, (int)pageNumber, currencyID, customerID, employeeID, orderStatusID, paymentMethodID, shipperID);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrderAsync(long id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            // Get response from business logic
            var response = await SalesService.GetOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("CreateOrderRequest")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            // Get response from business logic
            var response = await SalesService.GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequestModel value)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

            // Get response from business logic
            var response = await SalesService.CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesService.CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("Order/{id}")]
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
