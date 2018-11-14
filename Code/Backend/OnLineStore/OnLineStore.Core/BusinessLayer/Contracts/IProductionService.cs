using System.Threading.Tasks;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.EntityLayer.Production;

namespace OnLineStore.Core.BusinessLayer.Contracts
{
    public interface IProductionService : IService
    {
        Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null);

        Task<IPagedResponse<Warehouse>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1);

        Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string warehouseID = null);
    }
}
