using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.Extensions;
using Store.API.ViewModels;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;

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
        public async Task<IActionResult> GetOrders(Int32? pageSize = 10, Int32? pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrders));

            var response = await SalesBusinessObject.GetOrdersAsync((Int32)pageSize, (Int32)pageNumber);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Order/{id}")]
        public async Task<IActionResult> GetOrder(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrder));

            var response = await SalesBusinessObject.GetOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderViewModel()
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCreateOrderViewModel));

            var response = new SingleModelResponse<CreateOrderViewModel>() as ISingleModelResponse<CreateOrderViewModel>;

            var customersResponse = await SalesBusinessObject.GetCustomersAsync(0, 0);

            response.Model.Customers = customersResponse.Model.Select(item => new CustomerViewModel(item));

            var employeesResponse = await HumanResourcesBusinessObject.GetEmployeesAsync(0, 0);

            response.Model.Employees = employeesResponse.Model.Select(item => new EmployeeViewModel(item));

            var shippersResponse = await SalesBusinessObject.GetShippersAsync(0, 0);

            response.Model.Shippers = shippersResponse.Model.Select(item => new ShipperViewModel(item));

            var productsResponse = await ProductionBusinessObject.GetProductsAsync(0, 0);

            response.Model.Products = productsResponse.Model.Select(item => new ProductViewModel(item));

            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel value)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CreateOrder));

            var response = await SalesBusinessObject.CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrder(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CloneOrder));

            var response = await SalesBusinessObject.CloneOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("Order/{id}")]
        public async Task<IActionResult> RemoveOrder(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(RemoveOrder));

            var response = await SalesBusinessObject.RemoveOrderAsync(id);

            return response.ToHttpResponse();
        }
    }
}
