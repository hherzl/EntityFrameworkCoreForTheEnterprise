using System;
using System.Linq;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer
{
    public class ProductionBusinessObject : BusinessObject, IProductionBusinessObject
    {
        public ProductionBusinessObject(UserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IListModelResponse<Product> GetProducts(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Product>() as IListModelResponse<Product>;

            try
            {
                response.Model = ProductionRepository
                    .GetProducts(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
    }
}
