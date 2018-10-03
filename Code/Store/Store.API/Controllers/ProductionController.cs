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
        protected IProductionService ProductionService;

        public ProductionController(ILogger<ProductionController> logger, IProductionService productionService)
        {
            Logger = logger;
            ProductionService = productionService;
        }

        protected override void Dispose(bool disposing)
        {
            ProductionService?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet("Product")]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            // Get response from business logic
            var response = await ProductionService.GetProductsAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("ProductInventory")]
        public async Task<IActionResult> GetProductInventoryAsync(int? productID, string warehouseID)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductInventoryAsync));

            // Get response from business logic
            var response = await ProductionService.GetProductInventories(productID, warehouseID);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
