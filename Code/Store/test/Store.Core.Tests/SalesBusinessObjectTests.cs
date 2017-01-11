using System;
using System.Collections.Generic;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesBusinessObjectTests
    {
        [Fact]
        public void TestGetCustomers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetCustomers(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetShippers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetShippers(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetOrders()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetOrders(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestCreateOrder()
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
                var response = businessObject.CreateOrder(header, details.ToArray());

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestUpdateOrder()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var id = 1;

                // Act
                var response = businessObject.GetOrder(id);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
