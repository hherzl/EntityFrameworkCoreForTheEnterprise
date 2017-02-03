using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public class SalesBusinessObject : BusinessObject, ISalesBusinessObject
    {
        public SalesBusinessObject(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<IListModelResponse<Customer>> GetCustomersAsync(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Customer>() as IListModelResponse<Customer>;

            try
            {
                response.Model = await SalesRepository.GetCustomers(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IListModelResponse<Shipper>> GetShippersAsync(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Shipper>() as IListModelResponse<Shipper>;

            try
            {
                response.Model = await SalesRepository.GetShippers(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IListModelResponse<Order>> GetOrdersAsync(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Order>() as IListModelResponse<Order>;

            try
            {
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                response.Model = await SalesRepository.GetOrders(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleModelResponse<Order>> GetOrderAsync(Int32 id)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                response.Model = await SalesRepository.GetOrderAsync(new Order { OrderID = id });
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task <ISingleModelResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                using (var transaction = await DbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var detail in details)
                        {
                            var product = await ProductionRepository.GetProductAsync(new Product { ProductID = detail.ProductID });

                            if (product == null)
                            {
                                throw new NonExistingProductException(
                                    String.Format("Sent order has a non existing product with ID: '{0}', order has been cancelled.", detail.ProductID)
                                );
                            }
                            else
                            {
                                detail.ProductName = product.ProductName;
                            }

                            if (product.Discontinued == true)
                            {
                                throw new AddOrderWithDiscontinuedProductException(
                                    String.Format("Product with ID: '{0}' is discontinued, order has been cancelled.", product.ProductID)
                                );
                            }

                            detail.UnitPrice = product.UnitPrice;
                            detail.Total = product.UnitPrice * detail.Quantity;
                        }

                        header.Total = details.Sum(item => item.Total);

                        await SalesRepository.AddOrderAsync(header);

                        foreach (var detail in details)
                        {
                            detail.OrderID = header.OrderID;

                            await SalesRepository.AddOrderDetailAsync(detail);

                            var lastInventory = ProductionRepository
                                .GetProductInventories()
                                .Where(item => item.ProductID == detail.ProductID)
                                .OrderByDescending(item => item.EntryDate)
                                .FirstOrDefault();

                            var stocks = lastInventory == null ? 0 : lastInventory.Stocks - detail.Quantity;

                            var productInventory = new ProductInventory
                            {
                                ProductID = detail.ProductID,
                                EntryDate = DateTime.Now,
                                Quantity = detail.Quantity * -1,
                                Stocks = stocks
                            };

                            await ProductionRepository.AddProductInventoryAsync(productInventory);
                        }

                        response.Model = header;

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleModelResponse<Order>> CloneOrderAsync(Int32 id)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                var entity = await SalesRepository.GetOrderAsync(new Order { OrderID = id });

                if (entity != null)
                {
                    response.Model = new Order();

                    response.Model.OrderID = entity.OrderID;
                    response.Model.OrderDate = entity.OrderDate;
                    response.Model.CustomerID = entity.CustomerID;
                    response.Model.EmployeeID = entity.EmployeeID;
                    response.Model.ShipperID = entity.ShipperID;
                    response.Model.Total = entity.Total;
                    response.Model.Comments = entity.Comments;

                    if (entity.OrderDetails != null && entity.OrderDetails.Count > 0)
                    {
                        foreach (var detail in entity.OrderDetails)
                        {
                            response.Model.OrderDetails.Add(new OrderDetail
                            {
                                ProductID = detail.ProductID,
                                ProductName = detail.ProductName,
                                UnitPrice = detail.UnitPrice,
                                Quantity = detail.Quantity,
                                Total = detail.Total
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }
    }
}
