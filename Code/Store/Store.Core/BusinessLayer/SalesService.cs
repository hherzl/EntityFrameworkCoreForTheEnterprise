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
    public class SalesService : Service, ISalesService
    {
        public SalesService(ILogger<SalesService> logger, IUserInfo userInfo, StoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IPagedResponse<Customer>> GetCustomersAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCustomersAsync));

            var response = new PagedResponse<Customer>();

            try
            {
                // Get query
                var query = SalesRepository.GetCustomers();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
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

        public async Task<IPagedResponse<Shipper>> GetShippersAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetShippersAsync));

            var response = new PagedResponse<Shipper>();

            try
            {
                // Get query
                var query = SalesRepository.GetShippers();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
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

        public async Task<IPagedResponse<Currency>> GetCurrenciesAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCurrenciesAsync));

            var response = new PagedResponse<Currency>();

            try
            {
                // Get query
                var query = SalesRepository.GetCurrencies();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
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

        public async Task<IPagedResponse<PaymentMethod>> GetPaymentMethodsAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetPaymentMethodsAsync));

            var response = new PagedResponse<PaymentMethod>();

            try
            {
                // Get query
                var query = SalesRepository.GetPaymentMethods();

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
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

        public async Task<IPagedResponse<OrderInfo>> GetOrdersAsync(int pageSize = 10, int pageNumber = 1, Int16? currencyID = null, int? customerID = null, int? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, int? shipperID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            var response = new PagedResponse<OrderInfo>();

            try
            {
                // Get query
                var query = SalesRepository
                    .GetOrders(currencyID, customerID, employeeID, orderStatusID, paymentMethodID, shipperID);

                // Set information for paging
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
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

        public async Task<ISingleResponse<Order>> GetOrderAsync(long id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

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
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            var response = new SingleResponse<CreateOrderRequest>();

            try
            {
                // Retrieve customers list
                response.Model.Customers = await SalesRepository.GetCustomers().ToListAsync();

                // Retrieve employees list
                response.Model.Employees = await HumanResourcesRepository.GetEmployees().ToListAsync();

                // Retrieve shippers list
                response.Model.Shippers = await SalesRepository.GetShippers().ToListAsync();

                // Retrieve products list
                response.Model.Products = await ProductionRepository.GetProducts().ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

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
                                throw new NonExistingProductException(string.Format(SalesDisplays.NonExistingProductExceptionMessage, detail.ProductID));
                            }
                            else
                            {
                                // Set product name from existing entity
                                detail.ProductName = product.ProductName;
                            }

                            if (product.Discontinued == true)
                            {
                                // Throw exception if product is discontinued
                                throw new AddOrderWithDiscontinuedProductException(string.Format(SalesDisplays.AddOrderWithDiscontinuedProductExceptionMessage, product.ProductID));
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

        public async Task<ISingleResponse<Order>> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            var response = new SingleResponse<Order>();

            try
            {
                // Retrieve order by id
                var entity = await SalesRepository
                    .GetOrderAsync(new Order(id));

                if (entity != null)
                {
                    // Init a new instance for order
                    // Set values from existing order
                    response.Model = new Order
                    {
                        OrderID = entity.OrderID,
                        OrderDate = entity.OrderDate,
                        CustomerID = entity.CustomerID,
                        EmployeeID = entity.EmployeeID,
                        ShipperID = entity.ShipperID,
                        Total = entity.Total,
                        Comments = entity.Comments
                    };

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

        public async Task<ISingleResponse<Order>> RemoveOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(RemoveOrderAsync));

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
                        throw new ForeignKeyDependencyException(string.Format(SalesDisplays.RemoveOrderExceptionMessage, id));
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
