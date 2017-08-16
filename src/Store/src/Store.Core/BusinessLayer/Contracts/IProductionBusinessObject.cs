using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IProductionBusinessObject : IBusinessObject
    {
        Task<IPagingResponse<Product>> GetProductsAsync(Int32 pageSize = 10, Int32 pageNumber = 1, Int32? productCategoryID = null);

        Task<IPagingResponse<Warehouse>> GetWarehousesAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IListResponse<ProductInventory>> GetInventoryByProduct(Int32? productID);
    }
}
