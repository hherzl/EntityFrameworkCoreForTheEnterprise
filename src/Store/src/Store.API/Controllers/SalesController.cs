using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.Extensions;
using Store.API.ViewModels;
using Store.Core.BusinessLayer.Contracts;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        protected ILogger Logger;
        protected IHumanResourcesBusinessObject HumanResourcesBusinessObject;
        protected IProductionBusinessObject ProductionBusinessObject;
        protected ISalesBusinessObject SalesBusinessObject;

        public SalesController(ILogger<SalesController> logger, IHumanResourcesBusinessObject humanResourcesBusinessObject, IProductionBusinessObject productionBusinessObject, ISalesBusinessObject salesBusinessObject)
        {
            Logger = logger;
            HumanResourcesBusinessObject = humanResourcesBusinessObject;
            ProductionBusinessObject = productionBusinessObject;
            SalesBusinessObject = salesBusinessObject;
        }

        protected override void Dispose(Boolean disposing)
        {
            SalesBusinessObject?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet("Order")]
        public async Task<IActionResult> GetOrdersAsync(Int32? pageSize = 10, Int32? pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .GetOrdersAsync(
                    (Int32)pageSize,
                    (Int32)pageNumber,
                    currencyID: currencyID,
                    customerID: customerID,
                    employeeID: employeeID,
                    orderStatusID: orderStatusID,
                    paymentMethodID: paymentMethodID,
                    shipperID: shipperID
                );

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .GetOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderViewModel value)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("Order/{id}")]
        public async Task<IActionResult> DeleteOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(DeleteOrderAsync));

            // Get response from business logic
            var response = await SalesBusinessObject
                .RemoveOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
