using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.API.Extensions;
using Store.Core.BusinessLayer.Contracts;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductionController : Controller
    {
        protected ILogger Logger;
        protected IProductionBusinessObject ProductionBusinessObject;

        public ProductionController(ILogger<ProductionController> logger, IProductionBusinessObject productionBusinessObject)
        {
            Logger = logger;
            ProductionBusinessObject = productionBusinessObject;
        }

        protected override void Dispose(Boolean disposing)
        {
            ProductionBusinessObject?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> GetProductsAsync(Int32? pageSize = 10, Int32? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            var response = await ProductionBusinessObject.GetProductsAsync((Int32)pageSize, (Int32)pageNumber);

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("InventoryByProduct/{id}")]
        public async Task<IActionResult> GetInventoryByProduct(Int32 id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetInventoryByProduct));

            var response = await ProductionBusinessObject.GetInventoryByProduct(id);

            return response.ToHttpResponse();
        }
    }
}
