using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Requests;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.DataLayer.DataContracts;
using Store.Core.DataLayer.Repositories;
using Store.Core.EntityLayer.Dbo;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public class SalesBusinessObject : BusinessObject, ISalesBusinessObject
    {
        public SalesBusinessObject(ILogger logger, IUserInfo userInfo, StoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IPagingResponse<Customer>> GetCustomersAsync(Int32 pageSize = 0, Int32 pageNumber = 0)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCustomersAsync));

            var response = new PagingResponse<Customer>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetCustomers(pageSize, pageNumber);

                // Set information for paging
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IPagingResponse<Shipper>> GetShippersAsync(Int32 pageSize = 0, Int32 pageNumber = 0)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetShippersAsync));

            var response = new PagingResponse<Shipper>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetShippers(pageSize, pageNumber);

                // Set information for paging
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IPagingResponse<Currency>> GetCurrenciesAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCurrenciesAsync));

            var response = new PagingResponse<Currency>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetCurrencies(pageSize, pageNumber);

                // Set information for paging
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IPagingResponse<PaymentMethod>> GetPaymentMethodsAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetPaymentMethodsAsync));

            var response = new PagingResponse<PaymentMethod>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetPaymentMethods(pageSize, pageNumber);

                // Set information for paging
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query
                    .Paging(pageSize, pageNumber)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<IPagingResponse<OrderInfo>> GetOrdersAsync(Int32 pageSize = 10, Int32 pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrdersAsync));

            var response = new PagingResponse<OrderInfo>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetOrders(pageSize, pageNumber, currencyID, customerID, employeeID, orderStatusID, paymentMethodID, shipperID);

                // Set information for paging
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query
                    .Paging(pageSize, pageNumber)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<Order>> GetOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrderAsync));

            var response = new SingleResponse<Order>();

            try
            {
                response.Model = await SalesRepository.GetOrderAsync(new Order(id));
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<CreateOrderRequest>> GetCreateRequestAsync()
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCreateRequestAsync));

            var response = new SingleResponse<CreateOrderRequest>();

            try
            {
                response.Model.Customers = (await GetCustomersAsync()).Model;

                response.Model.Employees = (await HumanResourcesRepository.GetEmployees().ToListAsync());

                response.Model.Shippers = (await GetShippersAsync()).Model;

                response.Model.Products = (await ProductionRepository.GetProducts().ToListAsync());
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CreateOrderAsync));

            var response = new SingleResponse<Order>();

            try
            {
                using (var transaction = await DbContext.Database.BeginTransactionAsync())
                {
                    var warehouses = await ProductionRepository.GetWarehouses().ToListAsync();

                    try
                    {
                        foreach (var detail in details)
                        {
                            var product = await ProductionRepository.GetProductAsync(new Product { ProductID = detail.ProductID });

                            if (product == null)
                            {
                                throw new NonExistingProductException(
                                    String.Format(SalesDisplays.NonExistingProductExceptionMessage, detail.ProductID)
                                    );
                            }
                            else
                            {
                                detail.ProductName = product.ProductName;
                            }

                            if (product.Discontinued == true)
                            {
                                throw new AddOrderWithDiscontinuedProductException(
                                    String.Format(SalesDisplays.AddOrderWithDiscontinuedProductExceptionMessage, product.ProductID)
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
                                .OrderByDescending(item => item.CreationDateTime)
                                .FirstOrDefault();

                            var stocks = lastInventory == null ? 0 : lastInventory.Stocks - detail.Quantity;

                            var productInventory = new ProductInventory
                            {
                                ProductID = detail.ProductID,
                                WarehouseID = warehouses.First().WarehouseID,
                                CreationDateTime = DateTime.Now,
                                Quantity = detail.Quantity * -1,
                                Stocks = stocks
                            };

                            await ProductionRepository.AddProductInventoryAsync(productInventory);
                        }

                        response.Model = header;

                        transaction.Commit();

                        Logger.LogInformation(SalesDisplays.CreateOrderMessage);
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

        public async Task<ISingleResponse<Order>> CloneOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(CloneOrderAsync));

            var response = new SingleResponse<Order>();

            try
            {
                var entity = await SalesRepository.GetOrderAsync(new Order(id));

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

        public async Task<ISingleResponse<Order>> RemoveOrderAsync(Int32 id)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(RemoveOrderAsync));

            var response = new SingleResponse<Order>();

            try
            {
                response.Model = await SalesRepository.GetOrderAsync(new Order(id));

                if (response.Model != null)
                {
                    if (response.Model.OrderDetails.Count > 0)
                    {
                        throw new ForeignKeyDependencyException(String.Format(SalesDisplays.RemoveOrderExceptionMessage, id));
                    }

                    await SalesRepository.DeleteOrderAsync(response.Model);

                    Logger?.LogInformation(SalesDisplays.DeleteOrderMessage);
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
