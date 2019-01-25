using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.EntityLayer.Warehouse;

namespace OnlineStore.Core.DataLayer.Warehouse
{
    public static class OnlineStoreDbContextExtensions
    {
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

        public static async Task<EntityLayer.Warehouse.Location> GetWarehouseAsync(this OnlineStoreDbContext dbContext, EntityLayer.Warehouse.Location entity)
            => await dbContext.Warehouses.FirstOrDefaultAsync(item => item.LocationID == entity.LocationID);
    }
}
