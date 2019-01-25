using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.DataLayer;
using OnLineStore.Core.DataLayer.Repositories;
using OnLineStore.Core.DataLayer.Warehouse;
using OnLineStore.Core.EntityLayer.Warehouse;

namespace OnLineStore.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(ILogger<WarehouseService> logger, IUserInfo userInfo, OnLineStoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetProductsAsync));

            var response = new PagedResponse<Product>();

            try
            {
                // Get query
                var query = DbContext.GetProducts(productCategoryID);

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetProductsAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<EntityLayer.Warehouse.Location>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetWarehousesAsync));

            var response = new PagedResponse<EntityLayer.Warehouse.Location>();

            try
            {
                // Get query
                var query = DbContext.Warehouses;

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetWarehousesAsync), ex);
            }

            return response;
        }

        public async Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string warehouseID = null)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetProductInventories));

            var response = new ListResponse<ProductInventory>();

            try
            {
                // Get query
                var query = DbContext.GetProductInventories(productID, warehouseID);

                // Retrieve items, set model for response
                var list = await query.ToListAsync();

                // Sorting results
                response.Model = list.OrderByDescending(item => item.CreationDateTime);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetProductInventories), ex);
            }

            return response;
        }
    }
}
