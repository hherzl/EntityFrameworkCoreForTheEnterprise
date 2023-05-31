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

        [HttpGet("monthly-sale")]
        public async Task<IActionResult> GetMonthlySalesAsync()
        {
            var sales = await _reportsService.GetSalesAsync();
            var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

            var response = new MonthlySalesResponse();

            var now = DateTime.Now;

            foreach (var clientApplication in clientApplications)
            {
                var item = new MonthlySaleItemModel
                {
                    Year = $"{now.Year}",
                    ClientApplication = clientApplication
                };

                response.Sales.Add(item);

                for (var i = 0; i < response.Months.Count; i++)
                {
                    item.Month = response.Months[i];
                    item.Values.Add(
                        sales
                            .Where(item => item.CreatedOn.Value.Year == now.Year && item.CreatedOn.Value.Month == (i + 1) && item.ClientApplication == clientApplication)
                            .Sum(item => (double)item.Total)
                        );
                }
            }

            return Ok(response);
        }
    }
}
