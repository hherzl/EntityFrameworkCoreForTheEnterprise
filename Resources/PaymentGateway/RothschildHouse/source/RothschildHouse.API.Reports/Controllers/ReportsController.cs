using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Library.Common.Clients.Models.Reports;
using RothschildHouse.Library.Common.NoSql;

namespace RothschildHouse.API.Reports.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly SaleService _reportsService;

        public ReportsController(ILogger<ReportsController> logger, SaleService reportsService)
        {
            _logger = logger;
            _reportsService = reportsService;
        }

        [HttpGet("yearly-sale/{year}")]
        public async Task<IActionResult> GetYearlySalesAsync(int year)
        {
            _logger?.LogDebug($"'{nameof(GetYearlySalesAsync)}' has been invoked");

            var sales = await _reportsService.GetSalesAsync(year);
            var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

            var response = new YearlySalesResponse();

            var date = new DateTime(year, 1, 1);

            foreach (var clientApplication in clientApplications)
            {
                var item = new YearlySaleItemModel
                {
                    Year = $"{date.Year}",
                    ClientApplication = clientApplication
                };

                response.Sales.Add(item);

                for (var i = 0; i < response.Months.Count; i++)
                {
                    var total = sales
                        .Where(item => item.TxnDateTime.Value.Year == date.Year)
                        .Where(item => item.TxnDateTime.Value.Month == (i + 1))
                        .Where(item => item.ClientApplication == clientApplication)
                        .Sum(item => item.Total)
                        ;

                    item.Month = response.Months[i];
                    item.Values.Add(total ?? 0);
                }
            }

            return Ok(response);
        }

        [HttpGet("monthly-sale/{year}/{month}")]
        public async Task<IActionResult> GetMonthlySaleAsync(int year, int month)
        {
            _logger?.LogDebug($"'{nameof(GetMonthlySaleAsync)}' has been invoked");

            var sales = await _reportsService.GetSalesAsync(year, month);
            var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

            var response = new MonthlySalesResponse
            {
                Year = year,
                Month = month
            };

            foreach (var clientApplication in clientApplications)
            {
                response.Sales.Add(new MonthlySaleItemModel
                {
                    Year = $"{year}",
                    Month = $"{month}",
                    ClientApplication = clientApplication,
                    Total = sales
                        .Where(item => item.TxnDateTime.Value.Year == year)
                        .Where(item => item.TxnDateTime.Value.Month == month)
                        .Where(item => item.ClientApplication == clientApplication)
                        .Sum(item => item.Total)
                });
            }

            return Ok(response);
        }
    }
}
