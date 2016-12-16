using System;
using System.Collections.Generic;
using System.Linq;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.Core.Mocks
{
    public class OrderMockingTests
    {
        private void CreateData(DateTime startDate, DateTime endDate, Int32 ordersLimitPerDay)
        {
            var date = new DateTime(startDate.Year, startDate.Month, startDate.Day);

            while (date <= endDate)
            {
                if (date.DayOfWeek != DayOfWeek.Sunday)
                {
                    var random = new Random();

                    var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();
                    var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
                    var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();

                    var pageSize = 10;
                    var pageNumber = 1;

                    var customers = salesBusinessObject.GetCustomers(pageSize, pageNumber).Model.ToList();
                    var employees = humanResourcesBusinessObject.GetEmployees(pageSize, pageNumber).Model.ToList();
                    var shippers = salesBusinessObject.GetShippers(pageSize, pageNumber).Model.ToList();
                    var products = productionBusinessObject.GetProducts(pageSize, pageNumber).Model.ToList();

                    for (var i = 0; i < ordersLimitPerDay; i++)
                    {
                        var header = new Order();

                        var selectedCustomer = random.Next(0, customers.Count - 1);
                        var selectedEmployee = random.Next(0, employees.Count - 1);
                        var selectedShipper = random.Next(0, shippers.Count - 1);

                        header.CustomerID = customers[selectedCustomer].CustomerID;
                        header.EmployeeID = employees[selectedEmployee].EmployeeID;
                        header.ShipperID = shippers[selectedShipper].ShipperID;
                        header.OrderDate = date;
                        header.CreationDateTime = date;

                        var details = new List<OrderDetail>();

                        var detailsCount = random.Next(1, 3);

                        for (var j = 0; j < detailsCount; j++)
                        {
                            details.Add(new OrderDetail
                            {
                                ProductID = products[random.Next(0, products.Count - 1)].ProductID,
                                Quantity = (Int16)random.Next(1, 3)
                            });
                        }

                        salesBusinessObject.CreateOrder(header, details.ToArray());
                    }

                    salesBusinessObject.Dispose();
                    humanResourcesBusinessObject.Dispose();
                    productionBusinessObject.Dispose();
                }

                date = date.AddDays(1);
            }
        }

        [Fact]
        public void CreateOrders()
        {
            CreateData(
                startDate: new DateTime(DateTime.Now.Year, 1, 1),
                endDate: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)),
                ordersLimitPerDay: 10
            );
        }
    }
}
