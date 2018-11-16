using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnLineStore.Core.EntityLayer.Warehouse;

namespace OnLineStore.Core.DataLayer.Warehouse
{
    public static class OnLineStoreDbContextExtensions
    {
        public static IQueryable<Product> GetProducts(this OnLineStoreDbContext dbContext, int? productCategoryID = null)
        {
            var query = dbContext.Products.AsQueryable();

            if (productCategoryID.HasValue)
                query = query.Where(item => item.ProductCategoryID == productCategoryID);

            return query;
        }

        public static async Task<Product> GetProductAsync(this OnLineStoreDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);

        public static Product GetProductByName(this OnLineStoreDbContext dbContext, string productName)
            => dbContext.Products.FirstOrDefault(item => item.ProductName == productName);

        public static async Task<ProductCategory> GetProductCategoryAsync(this OnLineStoreDbContext dbContext, ProductCategory entity)
            => await dbContext.ProductCategories.FirstOrDefaultAsync(item => item.ProductCategoryID == entity.ProductCategoryID);

        public static IQueryable<ProductInventory> GetProductInventories(this OnLineStoreDbContext dbContext, int? productID = null, string warehouseID = null)
        {
            var query = dbContext.ProductInventories.AsQueryable();

            if (productID.HasValue)
                query = query.Where(item => item.ProductID == productID);

            if (!string.IsNullOrEmpty(warehouseID))
                query = query.Where(item => item.WarehouseID == warehouseID);

            return query;
        }

        public static async Task<ProductInventory> GetProductInventoryAsync(this OnLineStoreDbContext dbContext, ProductInventory entity)
            => await dbContext.ProductInventories.FirstOrDefaultAsync(item => item.ProductInventoryID == entity.ProductInventoryID);

        public static async Task<EntityLayer.Warehouse.Location> GetWarehouseAsync(this OnLineStoreDbContext dbContext, EntityLayer.Warehouse.Location entity)
            => await dbContext.Warehouses.FirstOrDefaultAsync(item => item.LocationID == entity.LocationID);
    }
}
