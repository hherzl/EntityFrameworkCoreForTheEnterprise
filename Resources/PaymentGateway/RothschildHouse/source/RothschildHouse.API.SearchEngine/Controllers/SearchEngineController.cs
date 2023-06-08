﻿using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.SearchEngine.Models;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.NoSql;
using RothschildHouse.Library.Common.NoSql.Documents;

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
        public async Task<IActionResult> IndexSaleAsync([FromBody] IndexSaleRequest request)
        {
            _logger?.LogDebug($"'{nameof(IndexSaleAsync)}' has been invoked");

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

            _logger?.LogInformation($"Indexing sale for payment transaction '{request.PaymentTxnId}'...");

            await _saleService.AddSaleAsync(document);

            _logger?.LogInformation($" Payment transaction '{request.PaymentTxnId}' was indexed successfully, Id: '{document.Id}'");

            return Ok(new CreatedResponse<string>(document.Id));
        }
    }
}
