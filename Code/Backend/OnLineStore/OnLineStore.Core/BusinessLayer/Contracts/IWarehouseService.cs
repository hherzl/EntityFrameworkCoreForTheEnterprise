using System.Threading.Tasks;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.EntityLayer.Warehouse;

namespace OnLineStore.Core.BusinessLayer.Contracts
{
    public interface IWarehouseService : IService
    {
        Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null);

        Task<IPagedResponse<Location>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1);

        Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string warehouseID = null);
    }
}
