using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesBusinessObjectTests
    {
        [Fact]
        public async Task TestGetCustomers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await businessObject.GetCustomersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestGetShippers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await businessObject.GetShippersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestGetOrders()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await businessObject.GetOrdersAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestCreateOrder()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
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
                var response = await businessObject.CreateOrderAsync(header, details.ToArray());

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestUpdateOrder()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var id = 1;

                // Act
                var response = await businessObject.GetOrderAsync(id);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestRemoveOrder()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var id = 600;

                // Act
                var response = await businessObject.RemoveOrderAsync(id);

                // Assert
                Assert.True(response.DidError);
                Assert.True(response.ErrorMessage == String.Format("Order with ID: {0} cannot be deleted, because has dependencies. Please contact to technical support for more details", id));
            }
        }
    }
}
