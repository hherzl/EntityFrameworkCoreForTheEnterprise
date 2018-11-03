using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.DataLayer;
using OnLineStore.Core.DataLayer.Repositories;
using OnLineStore.Core.EntityLayer.Production;

namespace OnLineStore.Core.BusinessLayer
{
    public class ProductionService : Service, IProductionService
    {
        public ProductionService(ILogger<ProductionService> logger, IUserInfo userInfo, StoreDbContext dbContext)
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
                var query = ProductionRepository.GetProducts(productCategoryID);

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IPagedResponse<Warehouse>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetWarehousesAsync));

            var response = new PagedResponse<Warehouse>();

            try
            {
                // Get query
                var query = ProductionRepository.GetWarehouses();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
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
                var query = ProductionRepository.GetProductInventories(productID, warehouseID);

                // Retrieve items, set model for response
                var list = await query.ToListAsync();

                // Sorting results
                response.Model = list.OrderByDescending(item => item.CreationDateTime);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }
    }
}
