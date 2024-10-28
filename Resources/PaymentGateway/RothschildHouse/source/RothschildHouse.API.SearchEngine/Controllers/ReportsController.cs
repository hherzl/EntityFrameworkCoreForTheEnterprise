using Microsoft.AspNetCore.Mvc;
using RothschildHouse.API.SearchEngine.Models;
using RothschildHouse.API.SearchEngine.Services;
using RothschildHouse.Domain.Entities;

namespace RothschildHouse.API.SearchEngine.Controllers;

public record ChartResponse
{
    public ChartResponse()
    {
        Labels = new();
        Datasets = new();
        BackgroundColors = new()
        {
             "#5491DA", "#E74C3C", "#82E0AA", "#E5E7E9"
        };
    }

    public List<string> Labels { get; set; }
    public List<DatasetModel> Datasets { get; set; }
    public List<string> BackgroundColors { get; set; }
}

public record DatasetModel
{
    public DatasetModel()
    {
        Data = new();
    }

    public List<decimal?> Data { get; set; }
    public string Label { get; set; }
    public string BackgroundColor { get; set; }
}

[ApiController]
[Route("api/v1")]
public class ReportsController : ControllerBase
{
    private readonly ILogger<SearchEngineController> _logger;
    private readonly SaleService _saleService;

    public ReportsController(ILogger<SearchEngineController> logger, SaleService saleService)
    {
        _logger = logger;
        _saleService = saleService;
    }

    [HttpGet("yearly-sale/{year}")]
    [ProducesResponseType(200, Type = typeof(ChartResponse))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetYearlySalesAsync(int year)
    {
        var sales = await _saleService.GetSalesAsync(year);
        var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

        var response = new ChartResponse();

        var date = new DateTime(year, 1, 1);

        response.Labels = new() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        foreach (var clientApplication in clientApplications)
        {
            var item = new DatasetModel
            {
                Label = clientApplication,
                BackgroundColor = response.BackgroundColors[new Random().Next(response.BackgroundColors.Count)]
            };

            for (var i = 0; i < response.Labels.Count; i++)
            {
                var total = sales
                    .Where(item => item.TxnDateTime.Value.Year == date.Year)
                    .Where(item => item.TxnDateTime.Value.Month == (i + 1))
                    .Where(item => item.ClientApplication == clientApplication)
                    .Sum(item => item.Total)
                    ;

                item.Data.Add(total ?? 0);
            }

            response.Datasets.Add(item);
        }

        return Ok(response);
    }

    //[HttpGet("yearly-sale/{year}")]
    //[ProducesResponseType(201, Type = typeof(YearlySalesResponse))]
    //[ProducesResponseType(500)]
    //public async Task<IActionResult> GetYearlySalesAsync(int year)
    //{
    //    var sales = await _saleService.GetSalesAsync(year);
    //    var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

    //    var response = new YearlySalesResponse();

    //    var date = new DateTime(year, 1, 1);

    //    foreach (var clientApplication in clientApplications)
    //    {
    //        var item = new YearlySaleItemModel
    //        {
    //            Year = $"{date.Year}",
    //            ClientApplication = clientApplication
    //        };

    //        response.Sales.Add(item);

    //        for (var i = 0; i < response.Months.Count; i++)
    //        {
    //            var total = sales
    //                .Where(item => item.TxnDateTime.Value.Year == date.Year)
    //                .Where(item => item.TxnDateTime.Value.Month == (i + 1))
    //                .Where(item => item.ClientApplication == clientApplication)
    //                .Sum(item => item.Total)
    //                ;

    //            item.Month = response.Months[i];
    //            item.Values.Add(total ?? 0);
    //        }
    //    }

    //    return Ok(response);
    //}

    //[HttpGet("monthly-sale/{year}/{month}")]
    //[ProducesResponseType(201, Type = typeof(MonthlySalesResponse))]
    //[ProducesResponseType(500)]
    //public async Task<IActionResult> GetMonthlySaleAsync(int year, int month)
    //{
    //    var sales = await _saleService.GetSalesAsync(year, month);
    //    var clientApplications = sales.Select(item => item.ClientApplication).Distinct().ToList();

    //    var response = new MonthlySalesResponse
    //    {
    //        Year = year,
    //        Month = month
    //    };

    //    foreach (var clientApplication in clientApplications)
    //    {
    //        response.Sales.Add(new MonthlySaleItemModel
    //        {
    //            Year = $"{year}",
    //            Month = $"{month}",
    //            ClientApplication = clientApplication,
    //            Total = sales
    //                .Where(item => item.TxnDateTime.Value.Year == year)
    //                .Where(item => item.TxnDateTime.Value.Month == month)
    //                .Where(item => item.ClientApplication == clientApplication)
    //                .Sum(item => item.Total)
    //        });
    //    }

    //    return Ok(response);
    //}
}
