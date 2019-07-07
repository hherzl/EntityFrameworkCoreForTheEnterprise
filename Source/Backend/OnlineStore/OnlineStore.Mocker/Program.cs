using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineStore.Common.Helpers;
using OnlineStore.Core.Domain.Sales;

namespace OnlineStore.Mocker
{
    public class Program
    {
        private static readonly ILogger Logger;

        static Program()
        {
            Logger = LoggingHelper.GetLogger<Program>();
        }

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var ordersLimitPerDay = 3;

            foreach (var arg in args)
            {
                if (arg.StartsWith("/year:"))
                    year = Convert.ToInt32(arg.Replace("/year:", string.Empty));
                else if (arg.StartsWith("/ordersLimitPerDay:"))
                    ordersLimitPerDay = Convert.ToInt32(arg.Replace("/ordersLimitPerDay:", string.Empty));
            }

            var start = new DateTime(year, 1, 1);
            var end = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));

            if (start.DayOfWeek == DayOfWeek.Sunday)
                start = start.AddDays(1);

            do
            {
                if (start.DayOfWeek != DayOfWeek.Sunday)
                {
                    await CreateDataAsync(start, ordersLimitPerDay);

                    Thread.Sleep(1000);
                }

                start = start.AddDays(1);
            }
            while (start <= end);
        }

        static async Task CreateDataAsync(DateTime date, int ordersLimitPerDay)
        {
            var random = new Random();

            var warehouseService = ServiceMocker.GetWarehouseService();
            var salesService = ServiceMocker.GetSalesService();

            var customers = (await salesService.GetCustomersAsync()).Model.ToList();
            var currencies = (await salesService.GetCurrenciesAsync()).Model.ToList();
            var paymentMethods = (await salesService.GetPaymentMethodsAsync()).Model.ToList();
            var products = (await warehouseService.GetProductsAsync()).Model.ToList();

            Logger.LogInformation("Creating orders for {0}", date);

            for (var i = 0; i < ordersLimitPerDay; i++)
            {
                var header = new OrderHeader
                {
                    OrderDate = date,
                    CreationDateTime = date
                };

                var selectedCustomer = random.Next(0, customers.Count - 1);
                var selectedCurrency = random.Next(0, currencies.Count - 1);
                var selectedPaymentMethod = random.Next(0, paymentMethods.Count - 1);

                header.CustomerID = customers[selectedCustomer].ID;
                header.CurrencyID = currencies[selectedCurrency].ID;
                header.PaymentMethodID = paymentMethods[selectedPaymentMethod].ID;

                var details = new List<OrderDetail>();

                var detailsCount = random.Next(1, 5);

                for (var j = 0; j < detailsCount; j++)
                {
                    var detail = new OrderDetail
                    {
                        ProductID = products[random.Next(0, products.Count - 1)].ID,
                        Quantity = (short)random.Next(1, 5)
                    };

                    if (details.Count > 0 && details.Count(item => item.ProductID == detail.ProductID) == 1)
                        continue;

                    details.Add(detail);
                }

                await salesService.CreateOrderAsync(header, details.ToArray());

                Logger.LogInformation("Date: {0}", date);
            }

            warehouseService.Dispose();
            salesService.Dispose();
        }
    }
}
