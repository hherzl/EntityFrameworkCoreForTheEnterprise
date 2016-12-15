using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Store.Core.BusinessLayer;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesBusinessObjectTests
    {
        private ISalesBusinessObject SalesBusinessObject
        {
            get
            {
                var userInfo = new UserInfo { Name = "admin" };

                var appSettings = Options.Create(AppSettingsMock.Default);

                var entityMapper = new StoreEntityMapper() as IEntityMapper;

                return new SalesBusinessObject(userInfo, new StoreDbContext(appSettings, entityMapper)) as ISalesBusinessObject;
            }
        }

        [Fact]
        public void TestGetOrders()
        {
            // Arrange
            using (var businessObject = SalesBusinessObject)
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
            using (var businessObject = SalesBusinessObject)
            {
                var header = new Order();

                header.OrderDate = DateTime.Now;
                header.CustomerID = 1;
                header.EmployeeID = 1;
                header.ShipperID = 1;

                var details = new List<OrderDetail>();

                details.Add(new OrderDetail
                {
                    ProductID = 1,
                    Quantity = 1,
                    UnitPrice = 1
                });

                // Act
                var response = businessObject.CreateOrder(header, details.ToArray());

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
