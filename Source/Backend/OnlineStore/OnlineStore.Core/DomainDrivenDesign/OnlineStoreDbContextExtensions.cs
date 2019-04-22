using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.DomainDrivenDesign.Dbo;
using OnlineStore.Core.DomainDrivenDesign.HumanResources;
using OnlineStore.Core.DomainDrivenDesign.Sales;
using OnlineStore.Core.DomainDrivenDesign.Warehouse;

namespace OnlineStore.Core.DomainDrivenDesign
{
    public static class OnlineStoreDbContextExtensions
    {
        public static void Add<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity, IUserInfo userInfo) where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity cast)
            {
                if (string.IsNullOrEmpty(cast.CreationUser))
                    cast.CreationUser = userInfo.UserName;

                if (!cast.CreationDateTime.HasValue)
                    cast.CreationDateTime = DateTime.Now;
            }

            dbContext.Set<TEntity>().Add(entity);
        }

        public static void Update<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity, IUserInfo userInfo) where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity cast)
            {
                if (string.IsNullOrEmpty(cast.LastUpdateUser))
                    cast.LastUpdateUser = userInfo.UserName;

                if (!cast.LastUpdateDateTime.HasValue)
                    cast.LastUpdateDateTime = DateTime.Now;
            }

            dbContext.Set<TEntity>().Update(entity);
        }

        public static void Remove<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity) where TEntity : class, IAuditableEntity
            => dbContext.Set<TEntity>().Remove(entity);

        private static IEnumerable<ChangeLog> GetChanges(this OnlineStoreDbContext dbContext, IUserInfo userInfo)
        {
            var exclusions = dbContext.ChangeLogExclusions.ToList();

            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Modified)
                    continue;

                var entityType = entry.Entity.GetType();

                if (exclusions.Count(item => item.EntityName == entityType.Name && item.PropertyName == "*") == 1)
                    yield break;

                foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                {
                    // Validate if there is an exclusion for *.Property
                    if (exclusions.Count(item => item.EntityName == "*" && string.Compare(item.PropertyName, property.Name, true) == 0) == 1)
                        continue;

                    // Validate if there is an exclusion for Entity.Property
                    if (exclusions.Count(item => item.EntityName == entityType.Name && string.Compare(item.PropertyName, property.Name, true) == 0) == 1)
                        continue;

                    var originalValue = entry.Property(property.Name).OriginalValue;
                    var currentValue = entry.Property(property.Name).CurrentValue;

                    if (string.Concat(originalValue) == string.Concat(currentValue))
                        continue;

                    // todo: improve the way to retrieve primary key value from entity instance

                    var key = entry.Entity.GetType().GetProperties().First().GetValue(entry.Entity, null).ToString();

                    yield return new ChangeLog
                    {
                        ClassName = entityType.Name,
                        PropertyName = property.Name,
                        Key = key,
                        OriginalValue = originalValue == null ? string.Empty : originalValue.ToString(),
                        CurrentValue = currentValue == null ? string.Empty : currentValue.ToString(),
                        UserName = userInfo.UserName,
                        ChangeDate = DateTime.Now
                    };
                }
            }
        }

        public static async Task<Employee> GetEmployeeAsync(this OnlineStoreDbContext dbContext, Employee entity)
            => await dbContext.Employees.FirstOrDefaultAsync(item => item.EmployeeID == entity.EmployeeID);

        public static async Task<OrderStatus> GetOrderStatusAsync(this OnlineStoreDbContext dbContext, OrderStatus entity)
           => await dbContext.OrderStatuses.FirstOrDefaultAsync(item => item.OrderStatusID == entity.OrderStatusID);

        public static async Task<Customer> GetCustomerAsync(this OnlineStoreDbContext dbContext, Customer entity)
            => await dbContext.Customers.FirstOrDefaultAsync(item => item.CustomerID == entity.CustomerID);

        public static async Task<Shipper> GetShipperAsync(this OnlineStoreDbContext dbContext, Shipper entity)
            => await dbContext.Shippers.FirstOrDefaultAsync(item => item.ShipperID == entity.ShipperID);

        public static IQueryable<OrderInfo> GetOrders(this OnlineStoreDbContext dbContext, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, string currencyID = null, Guid? paymentMethodID = null)
        {
            var query = from orderHeader in dbContext.OrderHeaders
                        join orderStatus in dbContext.OrderStatuses on orderHeader.OrderStatusID equals orderStatus.OrderStatusID

                        join customer in dbContext.Customers on orderHeader.CustomerID equals customer.CustomerID

                        join employeeJoin in dbContext.Employees on orderHeader.EmployeeID equals employeeJoin.EmployeeID into employeeTemp
                        from employee in employeeTemp.DefaultIfEmpty()

                        join currency in dbContext.Currencies on orderHeader.CurrencyID equals currency.CurrencyID

                        join paymentMethodJoin in dbContext.PaymentMethods on orderHeader.PaymentMethodID equals paymentMethodJoin.PaymentMethodID into paymentMethodTemp
                        from paymentMethod in paymentMethodTemp.DefaultIfEmpty()

                        join shipperJoin in dbContext.Shippers on orderHeader.ShipperID equals shipperJoin.ShipperID into shipperTemp
                        from shipper in shipperTemp.DefaultIfEmpty()

                        select new OrderInfo
                        {
                            OrderID = orderHeader.OrderHeaderID,
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
            => await dbContext.OrderHeaders.Include(p => p.OrderDetails).FirstOrDefaultAsync(item => item.OrderHeaderID == entity.OrderHeaderID);

        public static IQueryable<Product> GetProducts(this OnlineStoreDbContext dbContext, int? productCategoryID = null)
        {
            var query = dbContext.Products.AsQueryable();

            if (productCategoryID.HasValue)
                query = query.Where(item => item.ProductCategoryID == productCategoryID);

            return query;
        }

        public static async Task<Product> GetProductAsync(this OnlineStoreDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);

        public static Product GetProductByName(this OnlineStoreDbContext dbContext, string productName)
            => dbContext.Products.FirstOrDefault(item => item.ProductName == productName);

        public static async Task<ProductCategory> GetProductCategoryAsync(this OnlineStoreDbContext dbContext, ProductCategory entity)
            => await dbContext.ProductCategories.FirstOrDefaultAsync(item => item.ProductCategoryID == entity.ProductCategoryID);

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
            => await dbContext.ProductInventories.FirstOrDefaultAsync(item => item.ProductInventoryID == entity.ProductInventoryID);

        public static async Task<Location> GetWarehouseAsync(this OnlineStoreDbContext dbContext, Location entity)
            => await dbContext.Locations.FirstOrDefaultAsync(item => item.LocationID == entity.LocationID);
    }
}
