using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Extensions;
using Store.API.ViewModels;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        protected IHumanResourcesBusinessObject HumanResourcesBusinessObject;
        protected IProductionBusinessObject ProductionBusinessObject;
        protected ISalesBusinessObject SalesBusinessObject;

        public SalesController(IHumanResourcesBusinessObject humanResourcesBusinessObject, IProductionBusinessObject productionBusinessObject, ISalesBusinessObject salesBusinessObject)
        {
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
            var response = await SalesBusinessObject.GetOrdersAsync((Int32)pageSize, (Int32)pageNumber);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Order/{id}")]
        public async Task<IActionResult> GetOrder(Int32 id)
        {
            var response = await SalesBusinessObject.GetOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderViewModel()
        {
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
            var response = await SalesBusinessObject.CreateOrderAsync(value.GetOrder(), value.GetOrderDetails().ToArray());

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrder(Int32 id)
        {
            var response = await SalesBusinessObject.CloneOrderAsync(id);

            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("Order/{id}")]
        public async Task<IActionResult> RemoveOrder(Int32 id)
        {
            var response = await SalesBusinessObject.RemoveOrderAsync(id);

            return response.ToHttpResponse();
        }
    }
}
