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

        [HttpGet]
        [Route("Order")]
        public async Task<IActionResult> GetOrdersAsync(Int32? pageSize = 10, Int32? pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            var response = await SalesBusinessObject
                .GetOrdersAsync((Int32)pageSize, (Int32)pageNumber, currencyID: currencyID, customerID: customerID, employeeID: employeeID, orderStatusID: orderStatusID, paymentMethodID: paymentMethodID, shipperID: shipperID);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Order/{id}")]
        public async Task<IActionResult> GetOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            var response = await SalesBusinessObject
                .GetOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            var response = await SalesBusinessObject
                .GetCreateRequestAsync();

            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderViewModel value)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

            var response = await SalesBusinessObject
                .CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            var response = await SalesBusinessObject
                .CloneOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("Order/{id}")]
        public async Task<IActionResult> RemoveOrderAsync(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(RemoveOrderAsync));

            var response = await SalesBusinessObject
                .RemoveOrderAsync(id);

            return response.ToHttpResponse();
        }
    }
}
