using System;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Production;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IProductionBusinessObject : IBusinessObject
    {
        IListModelResponse<Product> GetProducts(Int32 pageSize, Int32 pageNumber);
    }
}
