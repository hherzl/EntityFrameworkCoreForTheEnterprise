using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.WebAPI.Responses;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
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
#pragma warning restore CS1591

        /// <summary>
        /// Retrieves the products
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>A sequence that contains the products</returns>
        [HttpGet("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            // Get response from business logic
            var response = await ProductionService.GetProductsAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Gets the inventory for product by warehouse
        /// </summary>
        /// <param name="productID">Product</param>
        /// <param name="warehouseID">Warehouse</param>
        /// <returns>A sequence of inventory transactions by product and warehouse</returns>
        [HttpGet("ProductInventory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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
