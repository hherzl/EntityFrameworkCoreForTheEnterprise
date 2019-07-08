using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStore.Common.Security;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.BusinessLayer.Requests;
using OnlineStore.WebAPI.Filters;
using OnlineStore.WebAPI.Requests;
using OnlineStore.WebAPI.Responses;

namespace OnlineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : OnlineStoreController
    {
        protected ILogger Logger;
        protected IWarehouseService Service;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService service)
            : base()
        {
            Logger = logger;
            Service = service;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Retrieves the products
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>A sequence that contains the products</returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            // Get response from business logic
            var response = await Service.GetProductsAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Gets the inventory for product by warehouse ID
        /// </summary>
        /// <param name="id">Product</param>
        /// <param name="warehouseID">Warehouse</param>
        /// <returns>A sequence of inventory transactions by product and warehouse</returns>
        /// <response code="200">Returns the inventory for product</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpGet("ProductInventory/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.WarehouseOperatorPolicy)]
        public async Task<IActionResult> GetProductInventoryAsync(int? id, string warehouseID)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductInventoryAsync));

            // Get response from business logic
            var response = await Service.GetProductInventories(id, warehouseID);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <returns>A sequence of inventory transactions by product and warehouse</returns>
        /// <response code="200">Returns the inventory for product</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPost("Product")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.WarehouseManagerPolicy)]
        public async Task<IActionResult> PostProductAsync(PostProductRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(PostProductAsync));

            var entity = request.GetProduct();

            entity.CreationUser = UserInfo.UserName;

            // Get response from business logic
            var response = await Service.CreateProductAsync(entity);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Updates the unit price for an existing product
        /// </summary>
        /// <returns>A confirmation for product unit price modification</returns>
        /// <response code="201">If unit price it was updated successfully</response>
        /// <response code="401">If client is not authenticated</response>
        /// <response code="403">If client is not autorized</response>
        /// <response code="404">If id is not exists</response>
        /// <response code="500">If there was an internal error</response>
        [HttpPut("ProductUnitPrice/{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [OnlineStoreActionFilter]
        [Authorize(Policy = Policies.WarehouseManagerPolicy)]
        public async Task<IActionResult> PutProductUnitPriceAsync(int? id, [FromBody]UpdateProductUnitPriceRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(PutProductUnitPriceAsync));

            // Get response from business logic
            Service.UserInfo = UserInfo;

            var response = await Service.UpdateProductUnitPriceAsync(id, request);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
