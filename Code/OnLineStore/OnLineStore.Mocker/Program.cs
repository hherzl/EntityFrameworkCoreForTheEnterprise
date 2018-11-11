using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnLineStore.Common;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.Mocker
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
            var task = new Task(MockAsync);

            task.Start();

            task.Wait();

            Console.ReadLine();
        }

        static async void MockAsync()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var ordersLimitPerDay = 5;

            var args = Environment.GetCommandLineArgs();

            foreach (var arg in args)
            {
                if (arg.StartsWith("/year:"))
                    year = Convert.ToInt32(arg.Replace("/year:", string.Empty));
                else if (arg.StartsWith("/ordersLimitPerDay:"))
                    ordersLimitPerDay = Convert.ToInt32(arg.Replace("/ordersLimitPerDay:", string.Empty));
            }

            var start = new DateTime(year, 1, 1);
            var end = new DateTime(year, 12, 31);

            if (start.DayOfWeek == DayOfWeek.Sunday)
                start = start.AddDays(1);

            while (start <= end)
            {
                Logger.LogInformation("Date: {0}", start);

                if (start.DayOfWeek != DayOfWeek.Sunday)
                {
                    await Task.Factory.StartNew(async () =>
                    {
                        await CreateDataAsync(start, ordersLimitPerDay);
                    });
                }

                start = start.AddDays(1);

                System.Threading.Thread.Sleep(1000);
            }
        }

        static async Task CreateDataAsync(DateTime date, int ordersLimitPerDay)
        {
            var random = new Random();

            var productionService = ServiceMocker.GetProductionService();
            var salesService = ServiceMocker.GetSalesService();

            var customers = (await salesService.GetCustomersAsync()).Model.ToList();
            var currencies = (await salesService.GetCurrenciesAsync()).Model.ToList();
            var paymentMethods = (await salesService.GetPaymentMethodsAsync()).Model.ToList();
            var products = (await productionService.GetProductsAsync()).Model.ToList();

            Logger.LogInformation("Creating orders for {0}", date);

            for (var i = 0; i < ordersLimitPerDay; i++)
            {
                var header = new Order();

                var selectedCustomer = random.Next(0, customers.Count - 1);
                var selectedCurrency = random.Next(0, currencies.Count - 1);
                var selectedPaymentMethod = random.Next(0, paymentMethods.Count - 1);

                header.CustomerID = customers[selectedCustomer].CustomerID;
                header.CurrencyID = currencies[selectedCurrency].CurrencyID;
                header.PaymentMethodID = paymentMethods[selectedPaymentMethod].PaymentMethodID;

                header.CreationDateTime = date;

                var details = new List<OrderDetail>();

                var detailsCount = random.Next(1, 2);

                for (var j = 0; j < detailsCount; j++)
                {
                    var detail = new OrderDetail
                    {
                        ProductID = products[random.Next(0, products.Count - 1)].ProductID,
                        Quantity = (short)random.Next(1, 2)
                    };

                    if (details.Count > 0 && details.Where(item => item.ProductID == detail.ProductID).Count() == 1)
                        continue;

                    details.Add(detail);
                }

                await salesService.CreateOrderAsync(header, details.ToArray());
            }

            productionService.Dispose();
            salesService.Dispose();
        }
    }
}
