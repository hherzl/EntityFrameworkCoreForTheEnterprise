using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core.BusinessLayer;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesServiceTests
    {
        [Fact]
        public async Task TestGetCustomers()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await service.GetCustomersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestGetShippers()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await service.GetShippersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestGetOrders()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await service.GetOrdersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestCreateOrder()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var header = new Order();

                header.OrderDate = DateTime.Now;
                header.OrderStatusID = 100;
                header.CustomerID = 1;
                header.EmployeeID = 1;
                header.ShipperID = 1;

                var details = new List<OrderDetail>();

                details.Add(new OrderDetail { ProductID = 1, Quantity = 1 });

                // Act
                var response = await service.CreateOrderAsync(header, details.ToArray());

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestUpdateOrder()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var id = 1;

                // Act
                var response = await service.GetOrderAsync(id);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestRemoveOrder()
        {
            // Arrange
            using (var service = ServiceMocker.GetSalesService())
            {
                var id = 601;

                // Act
                var response = await service.RemoveOrderAsync(id);

                // Assert
                Assert.True(response.DidError);
                Assert.True(response.ErrorMessage == String.Format(SalesDisplays.RemoveOrderExceptionMessage, id));
            }
        }
    }
}
