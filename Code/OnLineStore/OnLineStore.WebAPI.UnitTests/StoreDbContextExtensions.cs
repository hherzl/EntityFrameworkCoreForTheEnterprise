using System;
using System.Linq;
using OnLineStore.Core.DataLayer;
using OnLineStore.Core.EntityLayer.Dbo;
using OnLineStore.Core.EntityLayer.HumanResources;
using OnLineStore.Core.EntityLayer.Production;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.WebAPI.UnitTests
{
    public static class StoreDbContextExtensions
    {
        public static void SeedInMemory(this OnLineStoreDbContext dbContext)
        {
            var creationUser = "seed";
            var creationDateTime = DateTime.Now;

            var country = new Country
            {
                CountryID = 1,
                CountryName = "USA",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Country>().Add(country);

            var currency = new Currency
            {
                CurrencyID = 1000,
                CurrencyName = "US Dollar",
                CurrencySymbol = "$",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Currency>().Add(currency);

            dbContext.SaveChanges();

            var countryCurrency = new CountryCurrency
            {
                CountryID = country.CountryID,
                CurrencyID = currency.CurrencyID,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<CountryCurrency>().Add(countryCurrency);

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

            dbContext.Set<Employee>().Add(employee);

            dbContext.SaveChanges();

            var productCategory = new ProductCategory
            {
                ProductCategoryID = 1,
                ProductCategoryName = "PS4 Games",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<ProductCategory>().Add(productCategory);

            dbContext.SaveChanges();

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

            dbContext.Set<Product>().Add(product);

            dbContext.SaveChanges();

            var warehouse = new Warehouse
            {
                WarehouseID = "W0001",
                WarehouseName = "Warehouse 001",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Warehouse>().Add(warehouse);

            dbContext.SaveChanges();

            var productInventory = new ProductInventory
            {
                ProductID = product.ProductID,
                WarehouseID = warehouse.WarehouseID,
                Stocks = 1500,
                Quantity = 1500,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<ProductInventory>().Add(productInventory);

            dbContext.SaveChanges();

            var foo = dbContext.Set<ProductInventory>().ToList();

            var orderStatus = new OrderStatus
            {
                OrderStatusID = 100,
                Description = "Created",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<OrderStatus>().Add(orderStatus);

            dbContext.SaveChanges();

            var paymentMethod = new PaymentMethod
            {
                PaymentMethodID = Guid.NewGuid(),
                PaymentMethodDescription = "Credit Card",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<PaymentMethod>().Add(paymentMethod);

            dbContext.SaveChanges();

            var customer = new Customer
            {
                CustomerID = 1,
                CompanyName = "Best Buy",
                ContactName = "Colleen Dunn",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Customer>().Add(customer);

            dbContext.SaveChanges();

            var shipper = new Shipper
            {
                ShipperID = 1,
                CompanyName = "DHL",
                ContactName = "Ricardo A. Bartra",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Shipper>().Add(shipper);

            dbContext.SaveChanges();

            var order = new Order
            {
                OrderStatusID = 100,
                CustomerID = 1,
                EmployeeID = 1,
                ShipperID = 1,
                OrderDate = DateTime.Now,
                Total = 29.99m,
                CurrencyID = 1000,
                PaymentMethodID = paymentMethod.PaymentMethodID,
                Comments = "Order from mocks",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<Order>().Add(order);

            dbContext.SaveChanges();

            var orderDetail = new OrderDetail
            {
                OrderID = order.OrderID,
                ProductID = 1,
                ProductName = "The King of Fighters XIV",
                UnitPrice = 29.99m,
                Quantity = 1,
                Total = 29.99m,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Set<OrderDetail>().Add(orderDetail);

            dbContext.SaveChanges();
        }
    }
}
