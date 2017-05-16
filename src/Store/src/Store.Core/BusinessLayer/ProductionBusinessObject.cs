using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer
{
    public class ProductionBusinessObject : BusinessObject, IProductionBusinessObject
    {
        public ProductionBusinessObject(ILogger logger, IUserInfo userInfo, StoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IListModelResponse<Product>> GetProductsAsync(Int32 pageSize, Int32 pageNumber)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetProductsAsync));

            var response = new ListModelResponse<Product>() as IListModelResponse<Product>;

            try
            {
                response.Model = await ProductionRepository.GetProducts(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IListModelResponse<Warehouse>> GetWarehousesAsync(Int32 pageSize, Int32 pageNumber)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetWarehousesAsync));

            var response = new ListModelResponse<Warehouse>() as IListModelResponse<Warehouse>;

            try
            {
                response.Model = await ProductionRepository.GetWarehouses(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }
    }
}
