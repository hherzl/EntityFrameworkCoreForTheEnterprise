using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.SearchEngine.Services;
using RothschildHouse.API.SearchEngine.Services.Documents;
using RothschildHouse.Application.Clients.SearchEngine;
using RothschildHouse.Application.Common;

namespace RothschildHouse.API.SearchEngine.Controllers;

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
    [ProducesResponseType(201, Type = typeof(CreatedResponse<string>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> IndexSaleAsync([FromBody] IndexSaleRequest request)
    {
        var document = await _saleService.GetByTxnIdAsync(request.TxnId);

        _logger?.LogInformation($"Indexing sale for transaction '{request.TxnId}'...");

        if (document == null)
        {
            document = new SaleDocument
            {
                TxnId = request.TxnId,
                TxnGuid = request.TxnGuid,
                TxnDateTime = request.TxnDateTime,
                ClientApplicationId = request.ClientApplicationId,
                ClientApplication = request.ClientApplication,
                IssuingNetwork = request.IssuingNetwork,
                CardTypeId = request.CardTypeId,
                CardType = request.CardType,
                Total = request.Total,
                CurrencyId = request.CurrencyId,
                Currency = request.Currency,
                CreatedOn = DateTime.Now
            };

            await _saleService.AddSaleAsync(document);
        }
        else
        {
            await _saleService.UpdateAsync(request.TxnId, document);
        }

        _logger?.LogInformation($" Transaction '{request.TxnId}' was indexed successfully, Id: '{document.Id}'");

        return Ok(new CreatedResponse<string>(document.Id));
    }
}
