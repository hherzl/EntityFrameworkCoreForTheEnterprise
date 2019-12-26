using System.Threading.Tasks;
using OnlineStore.Core.Business.Requests;
using OnlineStore.Core.Business.Responses;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.Business.Contracts
{
    public interface IWarehouseService : IService
    {
        Task<IPagedResponse<Product>> GetProductsAsync(int pageSize = 10, int pageNumber = 1, int? productCategoryID = null);

        Task<IPagedResponse<Location>> GetWarehousesAsync(int pageSize = 10, int pageNumber = 1);

        Task<IListResponse<ProductInventory>> GetProductInventories(int? productID = null, string warehouseID = null);

        Task<ISingleResponse<Product>> CreateProductAsync(Product entity);

        Task<ISingleResponse<Product>> UpdateProductUnitPriceAsync(int? productID, UpdateProductUnitPriceRequest request);
    }
}
