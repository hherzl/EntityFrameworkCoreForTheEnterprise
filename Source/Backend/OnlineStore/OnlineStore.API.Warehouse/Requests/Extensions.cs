using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.API.Warehouse.Requests
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static Product GetProduct(this PostProductRequest request)
            => new Product
            {
                ID = request.ID,
                ProductName = request.ProductName,
                ProductCategoryID = request.ProductCategoryID,
                UnitPrice = request.UnitPrice,
                Description = request.Description
            };
    }
#pragma warning restore CS1591
}
