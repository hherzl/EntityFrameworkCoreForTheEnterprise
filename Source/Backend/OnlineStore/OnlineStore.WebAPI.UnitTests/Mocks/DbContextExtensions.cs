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
        static string creationUser;
        static DateTime creationDateTime;

        static DbContextExtensions()
        {
            creationUser = "unittests";
            creationDateTime = DateTime.Now;
        }

        public static OnlineStoreDbContext SeedCountries(this OnlineStoreDbContext dbContext)
        {
            var country = new Country
            {
                CountryID = 1,
                CountryName = "USA",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Countries.Add(country);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedCurrencies(this OnlineStoreDbContext dbContext)
        {
            var currency = new Currency
            {
                CurrencyID = "USD",
                CurrencyName = "US Dollar",
                CurrencySymbol = "$",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Currencies.Add(currency);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedCountryCurrencies(this OnlineStoreDbContext dbContext)
        {
            var countryCurrency = new CountryCurrency
            {
                CountryID = 1,
                CurrencyID = "USD",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.CountryCurrencies.Add(countryCurrency);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedEmployees(this OnlineStoreDbContext dbContext)
        {
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

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProductCategories(this OnlineStoreDbContext dbContext)
        {
            var productCategory = new ProductCategory
            {
                ProductCategoryID = 1,
                ProductCategoryName = "PS4 Games",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductCategories.Add(productCategory);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProducts(this OnlineStoreDbContext dbContext)
        {
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

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedLocations(this OnlineStoreDbContext dbContext)
        {
            var location = new Location
            {
                LocationID = "W01",
                LocationName = "Warehouse 01",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Warehouses.Add(location);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProductInventories(this OnlineStoreDbContext dbContext)
        {
            var productInventory = new ProductInventory
            {
                ProductID = 1000,
                LocationID = "W01",
                OrderDetailID = 1,
                Quantity = 1500,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductInventories.Add(productInventory);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedOrderStatuses(this OnlineStoreDbContext dbContext)
        {
            var orderStatus = new OrderStatus
            {
                OrderStatusID = 100,
                Description = "Created",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.OrderStatuses.Add(orderStatus);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedPaymentMethods(this OnlineStoreDbContext dbContext)
        {
            var paymentMethod = new PaymentMethod
            {
                PaymentMethodID = Guid.Parse("44C3737C-9993-448A-82F7-75C0E37E5A7F"),
                PaymentMethodDescription = "Credit Card",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.PaymentMethods.Add(paymentMethod);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedCustomers(this OnlineStoreDbContext dbContext)
        {
            var customer = new Customer
            {
                CustomerID = 1,
                CompanyName = "Best Buy",
                ContactName = "Colleen Dunn",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Customers.Add(customer);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedShippers(this OnlineStoreDbContext dbContext)
        {
            var shipper = new Shipper
            {
                ShipperID = 1,
                CompanyName = "DHL",
                ContactName = "Ricardo A. Bartra",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Shippers.Add(shipper);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedOrders(this OnlineStoreDbContext dbContext)
        {
            var order = new OrderHeader
            {
                OrderHeaderID = 1,
                OrderStatusID = 100,
                CustomerID = 1000,
                EmployeeID = 1,
                OrderDate = DateTime.Now,
                Total = 29.99m,
                CurrencyID = "USD",
                PaymentMethodID = Guid.Parse("44C3737C-9993-448A-82F7-75C0E37E5A7F"),
                Comments = "Order from unit tests",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Orders.Add(order);

            var orderDetail = new OrderDetail
            {
                OrderHeaderID = order.OrderHeaderID,
                ProductID = 1000,
                ProductName = "The King of Fighters XIV",
                UnitPrice = 29.99m,
                Quantity = 1,
                Total = 29.99m,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.OrderDetails.Add(orderDetail);

            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
