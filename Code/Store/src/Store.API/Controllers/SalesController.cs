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
            var response = await Task.Run(() =>
            {
                return SalesBusinessObject.GetOrders((Int32)pageSize, (Int32)pageNumber);
            });

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Order/{id}")]
        public async Task<IActionResult> GetOrder(Int32 id)
        {
            var response = await Task.Run(() =>
            {
                return SalesBusinessObject.GetOrder(id);
            });

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CreateOrderViewModel")]
        public async Task<IActionResult> GetCreateOrderViewModel()
        {
            var response = new SingleModelResponse<CreateOrderViewModel>() as ISingleModelResponse<CreateOrderViewModel>;

            response.Model.Customers = await Task.Run(() =>
            {
                return SalesBusinessObject.GetCustomers(0, 0).Model.Select(item => new CustomerViewModel(item));
            });

            response.Model.Employees = await Task.Run(() =>
            {
                return HumanResourcesBusinessObject.GetEmployees(0, 0).Model.Select(item => new EmployeeViewModel(item));
            });

            response.Model.Shippers = await Task.Run(() =>
            {
                return SalesBusinessObject.GetShippers(0, 0).Model.Select(item => new ShipperViewModel(item));
            });

            response.Model.Products = await Task.Run(() =>
            {
                return ProductionBusinessObject.GetProducts(0, 0).Model.Select(item => new ProductViewModel(item));
            });

            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderViewModel value)
        {
            var response = await Task.Run(() =>
            {
                return SalesBusinessObject.CreateOrder(value.GetOrder(), value.GetOrderDetails().ToArray());
            });

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CloneOrder/{id}")]
        public async Task<IActionResult> CloneOrder(Int32 id)
        {
            var response = await Task.Run(() =>
            {
                return SalesBusinessObject.CloneOrder(id);
            });

            return response.ToHttpResponse();
        }
    }
}
