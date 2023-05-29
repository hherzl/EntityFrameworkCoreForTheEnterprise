using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.SearchEngine.Models;
using RothschildHouse.API.SearchEngine.Services;
using RothschildHouse.API.SearchEngine.Services.Models;

namespace RothschildHouse.API.SearchEngine.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ILogger<SearchEngineController> _logger;
        private readonly SaleService _saleService;

        public SearchEngineController(ILogger<SearchEngineController> logger, SaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost("sale")]
        public async Task<IActionResult> PostSaleAsync([FromBody] IndexSaleRequest request)
        {
            var document = new SaleDocument
            {
                PaymentTxnId = request.PaymentTxnId,
                PaymentTxnGuid = request.PaymentTxnGuid,
                ClientApplicationId = request.ClientApplicationId,
                ClientApplication = request.ClientApplication,
                IssuingNetwork = request.IssuingNetwork,
                CardTypeId = request.CardTypeId,
                CardType = request.CardType,
                Total = request.Total,
                CurrencyId = request.CurrencyId,
                Currency = request.Currency,
                PaymentTxnDateTime = request.PaymentTxnDateTime,
                CreatedOn = DateTime.Now
            };

            await _saleService.AddSaleAsync(document);

            return Ok(new { document.Id });
        }
    }
}
