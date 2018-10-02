using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Controllers;
using Store.API.RequestModels;
using Store.Common;
using Store.Core.BusinessLayer.Requests;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.API.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestGetOrdersAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetOrdersAsync));
            var controller = new SalesController(logger, salesService);

            // Act
            var response = await controller.GetOrdersAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetOrdersByCurrencyAsync));
            var controller = new SalesController(logger, salesService);
            var currencyID = (short?)1000;

            // Act
            var response = await controller.GetOrdersAsync(currencyID: currencyID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
            Assert.True(value.Model.Where(item => item.CurrencyID == currencyID).Count() == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetOrdersByCustomerAsync));
            var controller = new SalesController(logger, salesService);
            var customerID = 1;

            // Act
            var response = await controller.GetOrdersAsync(customerID: customerID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Where(item => item.CustomerID == customerID).Count() == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetOrdersByEmployeeAsync));
            var controller = new SalesController(logger, salesService);
            var employeeID = 1;

            // Act
            var response = await controller.GetOrdersAsync(employeeID: employeeID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Where(item => item.EmployeeID == employeeID).Count() == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetOrderAsync));
            var controller = new SalesController(logger, salesService);
            var id = 1;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<Order>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetNonExistingOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetNonExistingOrderAsync));
            var controller = new SalesController(logger, salesService);
            var id = 0;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<Order>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestGetCreateOrderRequestAsync));
            var controller = new SalesController(logger, salesService);

            // Act
            var response = await controller.GetCreateOrderRequestAsync() as ObjectResult;
            var value = response.Value as ISingleResponse<CreateOrderRequest>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Products.Count() > 0);
            Assert.True(value.Model.Employees.Count() > 0);
            Assert.True(value.Model.Customers.Count() > 0);
            Assert.True(value.Model.Shippers.Count() > 0);
        }

        [Fact]
        public async Task TestCreateOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestCreateOrderAsync));
            var controller = new SalesController(logger, salesService);
            var model = new OrderRequestModel
            {
                OrderDate = DateTime.Now,
                CustomerID = 1,
                EmployeeID = 1,
                ShipperID = 1,
                Total = 29.99m,
                Comments = "Order from unit tests",
                CreationUser = "unitests",
                CreationDateTime = DateTime.Now,
                Details = new List<OrderDetailRequestModel>
                {
                    new OrderDetailRequestModel
                    {
                        ProductID = 1,
                        ProductName = "The King of Fighters XIV",
                        UnitPrice = 29.99m,
                        Quantity = 1,
                        Total = 29.99m
                    }
                }
            };

            // Act
            var response = await controller.CreateOrderAsync(model) as ObjectResult;
            var value = response.Value as ISingleResponse<Order>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.OrderID.HasValue);
        }

        [Fact]
        public async Task TestCloneOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService(nameof(TestCloneOrderAsync));
            var controller = new SalesController(logger, salesService);
            var id = 1;

            // Act
            var response = await controller.CloneOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<Order>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
        }
    }
}
