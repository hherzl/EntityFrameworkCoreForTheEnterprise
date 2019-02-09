using System;
using OnlineStore.Core.DataLayer;
using OnlineStore.Core.EntityLayer.Dbo;
using OnlineStore.Core.EntityLayer.HumanResources;
using OnlineStore.Core.EntityLayer.Sales;
using OnlineStore.Core.EntityLayer.Warehouse;

namespace OnlineStore.WebAPI.UnitTests.Mocks
{
    public static class DbContextExtensions
    {
        public static void SeedInMemory(this OnlineStoreDbContext dbContext)
        {
            var creationUser = "unittests";
            var creationDateTime = DateTime.Now;

            var country = new Country
            {
                CountryID = 1,
                CountryName = "USA",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Countries.Add(country);

            var currency = new Currency
            {
                CurrencyID = "USD",
                CurrencyName = "US Dollar",
                CurrencySymbol = "$",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Currencies.Add(currency);

            var countryCurrency = new CountryCurrency
            {
                CountryID = country.CountryID,
                CurrencyID = currency.CurrencyID,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.CountryCurrencies.Add(countryCurrency);

            dbContext.SaveChanges();

            var employee = new Employee
            {
                EmployeeID = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-25),
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Employees.Add(employee);

            dbContext.SaveChanges();

            var productCategory = new ProductCategory
            {
                ProductCategoryID = 1,
                ProductCategoryName = "PS4 Games",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductCategories.Add(productCategory);

            var product = new Product
            {
                ProductID = 1,
                ProductName = "The King of Fighters XIV",
                ProductCategoryID = 1,
                UnitPrice = 29.99m,
                Description = "KOF XIV",
                Discontinued = false,
                Stocks = 15000,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Products.Add(product);

            var location = new Location
            {
                LocationID = "W0001",
                LocationName = "Warehouse 001",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Warehouses.Add(location);

            var productInventory = new ProductInventory
            {
                ProductID = product.ProductID,
                LocationID = location.LocationID,
                OrderDetailID = 1,
                Quantity = 1500,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductInventories.Add(productInventory);

            dbContext.SaveChanges();

            var orderStatus = new OrderStatus
            {
                OrderStatusID = 100,
                Description = "Created",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.OrderStatuses.Add(orderStatus);

            var paymentMethod = new PaymentMethod
            {
                PaymentMethodID = Guid.NewGuid(),
                PaymentMethodDescription = "Credit Card",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.PaymentMethods.Add(paymentMethod);

            var customer = new Customer
            {
                CustomerID = 1,
                CompanyName = "Best Buy",
                ContactName = "Colleen Dunn",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Customers.Add(customer);

            var shipper = new Shipper
            {
                ShipperID = 1,
                CompanyName = "DHL",
                ContactName = "Ricardo A. Bartra",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Shippers.Add(shipper);

            var order = new OrderHeader
            {
                OrderStatusID = orderStatus.OrderStatusID,
                CustomerID = customer.CustomerID,
                EmployeeID = employee.EmployeeID,
                OrderDate = DateTime.Now,
                Total = 29.99m,
                CurrencyID = "USD",
                PaymentMethodID = paymentMethod.PaymentMethodID,
                Comments = "Order from unit tests",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Orders.Add(order);

            var orderDetail = new OrderDetail
            {
                OrderHeaderID = order.OrderHeaderID,
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = 29.99m,
                Quantity = 1,
                Total = 29.99m,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.OrderDetails.Add(orderDetail);

            dbContext.SaveChanges();
        }
    }
}
