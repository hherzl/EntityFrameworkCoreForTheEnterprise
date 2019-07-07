using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.HumanResources;
using OnlineStore.Core.Domain.Sales;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.Domain
{
    public static class OnlineStoreDbContextQueries
    {
        public static async Task<Employee> GetEmployeeAsync(this OnlineStoreDbContext dbContext, Employee entity)
            => await dbContext.Employees.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static IQueryable<Product> GetProducts(this OnlineStoreDbContext dbContext, int? productCategoryID = null)
        {
            var query = dbContext.Products.AsQueryable();

            if (productCategoryID.HasValue)
                query = query.Where(item => item.ProductCategoryID == productCategoryID);

            return query;
        }

        public static async Task<Product> GetProductAsync(this OnlineStoreDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static Product GetProductByName(this OnlineStoreDbContext dbContext, string productName)
            => dbContext.Products.FirstOrDefault(item => item.ProductName == productName);

        public static async Task<ProductCategory> GetProductCategoryAsync(this OnlineStoreDbContext dbContext, ProductCategory entity)
            => await dbContext.ProductCategories.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static IQueryable<ProductInventory> GetProductInventories(this OnlineStoreDbContext dbContext, int? productID = null, string locationID = null)
        {
            var query = dbContext.ProductInventories.AsQueryable();

            if (productID.HasValue)
                query = query.Where(item => item.ProductID == productID);

            if (!string.IsNullOrEmpty(locationID))
                query = query.Where(item => item.LocationID == locationID);

            return query;
        }

        public static async Task<ProductInventory> GetProductInventoryAsync(this OnlineStoreDbContext dbContext, ProductInventory entity)
            => await dbContext.ProductInventories.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static async Task<Location> GetLocationAsync(this OnlineStoreDbContext dbContext, Location entity)
            => await dbContext.Locations.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static async Task<OrderStatus> GetOrderStatusAsync(this OnlineStoreDbContext dbContext, OrderStatus entity)
           => await dbContext.OrderStatuses.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static async Task<Customer> GetCustomerAsync(this OnlineStoreDbContext dbContext, Customer entity)
            => await dbContext.Customers.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static async Task<Shipper> GetShipperAsync(this OnlineStoreDbContext dbContext, Shipper entity)
            => await dbContext.Shippers.FirstOrDefaultAsync(item => item.ID == entity.ID);

        public static IQueryable<OrderInfo> GetOrders(this OnlineStoreDbContext dbContext, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, string currencyID = null, Guid? paymentMethodID = null)
        {
            var query = from orderHeader in dbContext.OrderHeaders
                        join orderStatus in dbContext.OrderStatuses on orderHeader.OrderStatusID equals orderStatus.ID

                        join customer in dbContext.Customers on orderHeader.CustomerID equals customer.ID

                        join employeeJoin in dbContext.Employees on orderHeader.EmployeeID equals employeeJoin.ID into employeeTemp
                        from employee in employeeTemp.DefaultIfEmpty()

                        join currency in dbContext.Currencies on orderHeader.CurrencyID equals currency.ID

                        join paymentMethodJoin in dbContext.PaymentMethods on orderHeader.PaymentMethodID equals paymentMethodJoin.ID into paymentMethodTemp
                        from paymentMethod in paymentMethodTemp.DefaultIfEmpty()

                        join shipperJoin in dbContext.Shippers on orderHeader.ShipperID equals shipperJoin.ID into shipperTemp
                        from shipper in shipperTemp.DefaultIfEmpty()

                        select new OrderInfo
                        {
                            OrderID = orderHeader.ID,
                            OrderStatusID = orderHeader.OrderStatusID,
                            CustomerID = orderHeader.CustomerID,
                            EmployeeID = orderHeader.EmployeeID,
                            ShipperID = orderHeader.ShipperID,
                            OrderDate = orderHeader.OrderDate,
                            Total = orderHeader.Total,
                            CurrencyID = orderHeader.CurrencyID,
                            PaymentMethodID = orderHeader.PaymentMethodID,
                            Comments = orderHeader.Comments,
                            DetailsCount = orderHeader.DetailsCount,
                            ReferenceOrderID = orderHeader.ReferenceOrderID,
                            CreationUser = orderHeader.CreationUser,
                            CreationDateTime = orderHeader.CreationDateTime,
                            LastUpdateUser = orderHeader.LastUpdateUser,
                            LastUpdateDateTime = orderHeader.LastUpdateDateTime,
                            Timestamp = orderHeader.Timestamp,

                            OrderStatusDescription = orderStatus.Description,

                            CustomerCompanyName = customer == null ? string.Empty : customer.CompanyName,
                            CustomerContactName = customer == null ? string.Empty : customer.ContactName,

                            EmployeeFirstName = employee.FirstName,
                            EmployeeMiddleName = employee == null ? string.Empty : employee.MiddleName,
                            EmployeeLastName = employee.LastName,
                            EmployeeBirthDate = employee.BirthDate,

                            ShipperCompanyName = shipper == null ? string.Empty : shipper.CompanyName,
                            ShipperContactName = shipper == null ? string.Empty : shipper.ContactName,

                            CurrencyCurrencyName = currency == null ? string.Empty : currency.CurrencyName,
                            CurrencyCurrencySymbol = currency == null ? string.Empty : currency.CurrencySymbol,

                            PaymentMethodPaymentMethodName = paymentMethod == null ? string.Empty : paymentMethod.PaymentMethodName,
                            PaymentMethodPaymentMethodDescription = paymentMethod == null ? string.Empty : paymentMethod.PaymentMethodDescription
                        };

            if (orderStatusID.HasValue)
                query = query.Where(item => item.OrderStatusID == orderStatusID);

            if (customerID.HasValue)
                query = query.Where(item => item.CustomerID == customerID);

            if (employeeID.HasValue)
                query = query.Where(item => item.EmployeeID == employeeID);

            if (shipperID.HasValue)
                query = query.Where(item => item.ShipperID == shipperID);

            if (!string.IsNullOrEmpty(currencyID))
                query = query.Where(item => item.CurrencyID == currencyID);

            if (paymentMethodID.HasValue)
                query = query.Where(item => item.PaymentMethodID == paymentMethodID);

            return query;
        }

        public static async Task<OrderHeader> GetOrderAsync(this OnlineStoreDbContext dbContext, OrderHeader entity)
            => await dbContext.OrderHeaders.Include(p => p.OrderDetails).FirstOrDefaultAsync(item => item.ID == entity.ID);
    }
}
