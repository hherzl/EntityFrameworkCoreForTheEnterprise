using System;
using System.Linq;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public class SalesBusinessObject : BusinessObject, ISalesBusinessObject
    {
        public SalesBusinessObject(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public IListModelResponse<Order> GetOrders(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Order>() as IListModelResponse<Order>;

            try
            {
                response.Model = SalesRepository
                    .GetOrders(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public void CreateOrder(Order header, OrderDetail[] details)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    SalesRepository.AddOrder(header);

                    foreach(var detail in details)
                    {
                        detail.OrderID = header.OrderID;

                        SalesRepository.AddOrderDetail(detail);

                        var productInventory = new ProductInventory
                        {
                            ProductID= detail.ProductID,
                            EntryDate = DateTime.Now,
                            Quantity = detail.Quantity
                        };

                        ProductionRepository.AddProductInventory(productInventory);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex;
                }
            }
        }
    }
}
