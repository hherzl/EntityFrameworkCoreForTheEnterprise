using System.Threading.Tasks;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.EntityLayer.Warehouse;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface IWarehouseService : IService
    {
        Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null);

        Task<IPagedResponse<Location>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1);

        Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string warehouseID = null);
    }
}
