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

        public static IQueryable<CustomerOrderInfo> GetOrdersForCustomer(this OnlineStoreDbContext dbContext, int? customerID, short? orderStatusID = null)
        {
            var query =
                from orderHeader in dbContext.OrderHeaders
                join orderStatus in dbContext.OrderStatuses on orderHeader.OrderStatusID equals orderStatus.ID
                join currency in dbContext.Currencies on orderHeader.CurrencyID equals currency.ID
                where orderHeader.CustomerID == customerID
                select new CustomerOrderInfo
                {
                    OrderID = orderHeader.ID,
                    OrderDate = orderHeader.OrderDate,
                    OrderStatusID = orderHeader.OrderStatusID,
                    OrderStatusDescription = orderStatus.Description,
                    Total = orderHeader.Total,
                    CurrencyID = orderHeader.CurrencyID,
                    DetailsCount = orderHeader.DetailsCount
                };

            if (orderStatusID.HasValue)
                query = query.Where(item => item.OrderStatusID == orderStatusID);

            return query;
        }

        public static async Task<OrderHeader> GetOrderAsync(this OnlineStoreDbContext dbContext, OrderHeader entity)
            => await dbContext.OrderHeaders.Include(p => p.OrderDetails).FirstOrDefaultAsync(item => item.ID == entity.ID);
    }
}
