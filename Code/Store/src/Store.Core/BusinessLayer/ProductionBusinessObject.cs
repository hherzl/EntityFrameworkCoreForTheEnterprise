using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer
{
    public class ProductionBusinessObject : BusinessObject, IProductionBusinessObject
    {
        public ProductionBusinessObject(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<IListModelResponse<Product>> GetProductsAsync(Int32 pageSize, Int32 pageNumber)
        {
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
    }
}
