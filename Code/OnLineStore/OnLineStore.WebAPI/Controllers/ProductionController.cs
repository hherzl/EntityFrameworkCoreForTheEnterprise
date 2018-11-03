using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.WebAPI.Responses;

namespace OnLineStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductionController : ControllerBase
    {
        protected ILogger Logger;
        protected IProductionService ProductionService;

        public ProductionController(ILogger<ProductionController> logger, IProductionService productionService)
        {
            Logger = logger;
            ProductionService = productionService;
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
