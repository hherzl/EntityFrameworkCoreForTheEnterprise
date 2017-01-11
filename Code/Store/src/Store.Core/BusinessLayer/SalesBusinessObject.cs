using System;
using System.Linq;
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

        public IListModelResponse<Customer> GetCustomers(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Customer>() as IListModelResponse<Customer>;

            try
            {
                response.Model = SalesRepository
                    .GetCustomers(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public IListModelResponse<Shipper> GetShippers(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Shipper>() as IListModelResponse<Shipper>;

            try
            {
                response.Model = SalesRepository
                    .GetShippers(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public IListModelResponse<Order> GetOrders(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Order>() as IListModelResponse<Order>;

            try
            {
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                response.Model = SalesRepository
                    .GetOrders(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public ISingleModelResponse<Order> GetOrder(Int32 id)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                response.Model = SalesRepository.GetOrder(new Order { OrderID = id });
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public ISingleModelResponse<Order> CreateOrder(Order header, OrderDetail[] details)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var detail in details)
                        {
                            var product = ProductionRepository.GetProduct(new Product { ProductID = detail.ProductID });

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

                        SalesRepository.AddOrder(header);

                        foreach (var detail in details)
                        {
                            detail.OrderID = header.OrderID;

                            SalesRepository.AddOrderDetail(detail);

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

                            ProductionRepository.AddProductInventory(productInventory);
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

        public ISingleModelResponse<Order> CloneOrder(Int32 id)
        {
            var response = new SingleModelResponse<Order>() as ISingleModelResponse<Order>;

            try
            {
                var entity = SalesRepository.GetOrder(new Order { OrderID = id });

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
