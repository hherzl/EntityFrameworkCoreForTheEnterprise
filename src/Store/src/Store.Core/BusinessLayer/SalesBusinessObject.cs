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

        public async Task<IPagedResponse<Customer>> GetCustomersAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCustomersAsync));

            var response = new PagedResponse<Customer>();

            try
            {
                // Get query
                var query = SalesRepository.GetCustomers();

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

        public async Task<IPagedResponse<Shipper>> GetShippersAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetShippersAsync));

            var response = new PagedResponse<Shipper>();

            try
            {
                // Get query
                var query = SalesRepository.GetShippers();

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

        public async Task<IPagedResponse<Currency>> GetCurrenciesAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCurrenciesAsync));

            var response = new PagedResponse<Currency>();

            try
            {
                // Get query
                var query = SalesRepository.GetCurrencies();

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

        public async Task<IPagedResponse<PaymentMethod>> GetPaymentMethodsAsync(Int32 pageSize = 10, Int32 pageNumber = 1)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetPaymentMethodsAsync));

            var response = new PagedResponse<PaymentMethod>();

            try
            {
                // Get query
                var query = SalesRepository.GetPaymentMethods();

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

        public async Task<IPagedResponse<OrderInfo>> GetOrdersAsync(Int32 pageSize = 10, Int32 pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetOrdersAsync));

            var response = new PagedResponse<OrderInfo>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetOrders(currencyID, customerID, employeeID, orderStatusID, paymentMethodID, shipperID);

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
                // Retrieve order by id
                response.Model = await SalesRepository
                    .GetOrderAsync(new Order(id));
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<CreateOrderRequest>> GetCreateOrderRequestAsync()
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            var response = new SingleResponse<CreateOrderRequest>();

            try
            {
                // Retrieve customers list
                response.Model.Customers = (await GetCustomersAsync()).Model;

                // Retrieve employees list
                response.Model.Employees = (await HumanResourcesRepository.GetEmployees().ToListAsync());

                // Retrieve shippers list
                response.Model.Shippers = (await GetShippersAsync()).Model;

                // Retrieve products list
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
                // Begin transaction
                using (var transaction = await DbContext.Database.BeginTransactionAsync())
                {
                    // Retrieve warehouses
                    var warehouses = await ProductionRepository
                        .GetWarehouses()
                        .ToListAsync();

                    try
                    {
                        foreach (var detail in details)
                        {
                            // Retrieve product by id
                            var product = await ProductionRepository
                                .GetProductAsync(new Product(detail.ProductID));

                            if (product == null)
                            {
                                // Throw exception if product no exists
                                throw new NonExistingProductException(String.Format(SalesDisplays.NonExistingProductExceptionMessage, detail.ProductID));
                            }
                            else
                            {
                                // Set product name from existing entity
                                detail.ProductName = product.ProductName;
                            }

                            if (product.Discontinued == true)
                            {
                                // Throw exception if product is discontinued
                                throw new AddOrderWithDiscontinuedProductException(String.Format(SalesDisplays.AddOrderWithDiscontinuedProductExceptionMessage, product.ProductID));
                            }

                            // Set unit price and total for product detail
                            detail.UnitPrice = product.UnitPrice;
                            detail.Total = product.UnitPrice * detail.Quantity;
                        }

                        // Calculate total for order header from order's details
                        header.Total = details.Sum(item => item.Total);

                        // Save order header
                        await SalesRepository.AddOrderAsync(header);

                        foreach (var detail in details)
                        {
                            // Set order id for order detail
                            detail.OrderID = header.OrderID;

                            // Add order detail
                            await SalesRepository.AddOrderDetailAsync(detail);

                            // Get last inventory for product
                            var lastInventory = ProductionRepository
                                .GetProductInventories()
                                .Where(item => item.ProductID == detail.ProductID)
                                .OrderByDescending(item => item.CreationDateTime)
                                .FirstOrDefault();

                            // Calculate stocks for product
                            var stocks = lastInventory == null ? 0 : lastInventory.Stocks - detail.Quantity;

                            // Create product inventory instance
                            var productInventory = new ProductInventory
                            {
                                ProductID = detail.ProductID,
                                WarehouseID = warehouses.First().WarehouseID,
                                CreationDateTime = DateTime.Now,
                                Quantity = detail.Quantity * -1,
                                Stocks = stocks
                            };

                            // Save product inventory
                            await ProductionRepository.AddProductInventoryAsync(productInventory);
                        }

                        response.Model = header;

                        // Commit transaction
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
                // Retrieve order by id
                var entity = await SalesRepository
                    .GetOrderAsync(new Order(id));

                if (entity != null)
                {
                    // Init a new instance for order
                    response.Model = new Order();

                    // Set values from existing order
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
                            // Add order detail clone to collection
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
                // Retrieve order by id
                response.Model = await SalesRepository
                    .GetOrderAsync(new Order(id));

                if (response.Model != null)
                {
                    if (response.Model.OrderDetails.Count > 0)
                    {
                        // Restrict remove operation for orders with details
                        throw new ForeignKeyDependencyException(String.Format(SalesDisplays.RemoveOrderExceptionMessage, id));
                    }

                    // Delete order
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
