using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.BusinessLayer.Requests;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.Domain;
using OnlineStore.Core.Domain.Repositories;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(ILogger<WarehouseService> logger, OnlineStoreDbContext dbContext, IUserInfo userInfo)
            : base(logger, dbContext, userInfo)
        {
        }

        public async Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null)
        {
            Logger?.LogInformation("'{0}' has been invoked", nameof(GetProductsAsync));

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

        public async Task<IPagedResponse<Location>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogInformation("'{0}' has been invoked", nameof(GetWarehousesAsync));

            var response = new PagedResponse<Location>();

            try
            {
                // Get query
                var query = DbContext.Locations;

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

        public async Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string locationID = null)
        {
            Logger?.LogInformation("'{0}' has been invoked", nameof(GetProductInventories));

            var response = new ListResponse<ProductInventory>();

            try
            {
                // Get query
                var query = DbContext.GetProductInventories(productID, locationID);

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

        public async Task<ISingleResponse<Product>> CreateProductAsync(Product entity)
        {
            Logger?.LogInformation("'{0}' has been invoked", nameof(CreateProductAsync));

            var response = new SingleResponse<Product>();

            try
            {
                // Set default values
                entity.Stocks = 0;
                entity.Discontinued = false;

                // Set creation info
                DbContext.Add(entity, UserInfo);

                // Save product
                await DbContext.SaveChangesAsync();

                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(CreateProductAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<Product>> UpdateProductUnitPriceAsync(int? productID, UpdateProductUnitPriceRequest request)
        {
            Logger?.LogInformation("'{0}' has been invoked", nameof(UpdateProductUnitPriceAsync));

            var response = new SingleResponse<Product>();

            try
            {
                // Retrieve product by ID
                var entity = await DbContext.GetProductAsync(new Product(productID));

                if (entity == null)
                    return response;

                var history = new ProductUnitPriceHistory
                {
                    ProductID = entity.ID,
                    UnitPrice = entity.UnitPrice
                };

                // Change unit price
                if (entity.UnitPrice != request.UnitPrice)
                    entity.UnitPrice = request.UnitPrice;

                // Set last update info

                DbContext.Update(entity, UserInfo);

                DbContext.Add(history, UserInfo);

                // Save changes
                await DbContext.SaveChangesAsync();

                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(CreateProductAsync), ex);
            }

            return response;
        }
    }
}
