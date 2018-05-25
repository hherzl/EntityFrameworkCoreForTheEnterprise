using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.Responses;
using Store.Core.BusinessLayer.Contracts;

namespace Store.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductionController : Controller
    {
        protected ILogger Logger;
        protected IProductionService ProductionBusinessObject;

        public ProductionController(ILogger<ProductionController> logger, IProductionService productionBusinessObject)
        {
            Logger = logger;
            ProductionBusinessObject = productionBusinessObject;
        }

        protected override void Dispose(bool disposing)
        {
            ProductionBusinessObject?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet("Product")]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            // Get response from business logic
            var response = await ProductionBusinessObject.GetProductsAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("InventoryByProduct/{id}")]
        public async Task<IActionResult> GetInventoryByProduct(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetInventoryByProduct));

            // Get response from business logic
            var response = await ProductionBusinessObject.GetInventoryByProduct(id);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
