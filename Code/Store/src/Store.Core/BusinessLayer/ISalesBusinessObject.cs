using System;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public interface ISalesBusinessObject :  IBusinessObject
    {
        IListModelResponse<Order> GetOrders(Int32 pageSize, Int32 pageNumber);

        void CreateOrder(Order header, OrderDetail[] details);
    }
}
