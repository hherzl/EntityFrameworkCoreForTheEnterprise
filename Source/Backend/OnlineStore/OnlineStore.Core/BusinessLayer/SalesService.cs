using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.BusinessLayer.Requests;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.Domain;
using OnlineStore.Core.Domain.Dbo;
using OnlineStore.Core.Domain.Repositories;
using OnlineStore.Core.Domain.Sales;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.BusinessLayer
{
    public class SalesService : Service, ISalesService
    {
        public SalesService(ILogger<SalesService> logger, OnlineStoreDbContext dbContext, IUserInfo userInfo)
            : base(logger, dbContext, userInfo)
        {
        }

        public async Task<IPagedResponse<Customer>> GetCustomersAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetCustomersAsync));

            var response = new PagedResponse<Customer>();

            try
            {
                // Get query
                var query = DbContext.Customers;

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
                response.SetError(Logger, nameof(GetCustomersAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<Shipper>> GetShippersAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetShippersAsync));

            var response = new PagedResponse<Shipper>();

            try
            {
                // Get query
                var query = DbContext.Shippers;

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
                response.SetError(Logger, nameof(GetShippersAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<Currency>> GetCurrenciesAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetCurrenciesAsync));

            var response = new PagedResponse<Currency>();

            try
            {
                // Get query
                var query = DbContext.Currencies;

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
                response.SetError(Logger, nameof(GetCurrenciesAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<PaymentMethod>> GetPaymentMethodsAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetPaymentMethodsAsync));

            var response = new PagedResponse<PaymentMethod>();

            try
            {
                // Get query
                var query = DbContext.PaymentMethods;

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
                response.SetError(Logger, nameof(GetPaymentMethodsAsync), ex);
            }

            return response;
        }

        public async Task<IPagedResponse<OrderInfo>> GetOrdersAsync(SearchOrdersRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetOrdersAsync));

            var response = new PagedResponse<OrderInfo>();

            try
            {
                // Get query
                var query = DbContext
                    .GetOrders(request.OrderStatusID, request.CustomerID, request.EmployeeID, request.ShipperID, request.CurrencyID, request.PaymentMethodID);

                // Set information for paging
                response.PageSize = (int)request.PageSize;
                response.PageNumber = (int)request.PageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response
                response.Model = await query
                    .Paging((int)request.PageSize, (int)request.PageNumber)
                    .ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of rows: {2}", response.PageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetOrdersAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<OrderHeader>> GetOrderAsync(long id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetOrderAsync));

            var response = new SingleResponse<OrderHeader>();

            try
            {
                // Retrieve order by id
                response.Model = await DbContext.GetOrderAsync(new OrderHeader(id));
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetOrderAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<CreateOrderRequest>> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetCreateOrderRequestAsync));

            var response = new SingleResponse<CreateOrderRequest>();

            try
            {
                // Retrieve products list
                response.Model.Products = await DbContext.GetProducts().ToListAsync();

                // Retrieve customers list
                response.Model.Customers = await DbContext.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(GetCreateOrderRequestAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<OrderHeader>> CreateOrderAsync(OrderHeader header, OrderDetail[] details)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(CreateOrderAsync));

            var response = new SingleResponse<OrderHeader>();

            // Begin transaction
            using (var transaction = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // todo: Retrieve available warehouse to dispatch products
                    var warehouses = await DbContext.Locations.ToListAsync();

                    foreach (var detail in details)
                    {
                        // Retrieve product by id
                        var product = await DbContext.GetProductAsync(new Product(detail.ProductID));

                        // Throw exception if product no exists
                        if (product == null)
                            throw new NonExistingProductException(string.Format(SalesDisplays.NonExistingProductExceptionMessage, detail.ProductID));

                        // Throw exception if product is discontinued
                        if (product.Discontinued == true)
                            throw new AddOrderWithDiscontinuedProductException(string.Format(SalesDisplays.AddOrderWithDiscontinuedProductExceptionMessage, product.ID));

                        // Throw exception if quantity for product is invalid
                        if (detail.Quantity <= 0)
                            throw new InvalidQuantityException(string.Format(SalesDisplays.InvalidQuantityExceptionMessage, product.ID));

                        // Set values for detail
                        detail.ProductName = product.ProductName;
                        detail.UnitPrice = product.UnitPrice;
                        detail.Total = product.UnitPrice * detail.Quantity;
                    }

                    // Set default values for order header
                    if (!header.OrderDate.HasValue)
                        header.OrderDate = DateTime.Now;

                    header.OrderStatusID = 100;

                    // Calculate total for order header from order's details
                    header.Total = details.Sum(item => item.Total);
                    header.DetailsCount = details.Count();

                    // Save order header
                    DbContext.Add(header, UserInfo);

                    await DbContext.SaveChangesAsync();

                    foreach (var detail in details)
                    {
                        // Set order id for order detail
                        detail.OrderHeaderID = header.ID;
                        detail.CreationUser = header.CreationUser;

                        // Add order detail
                        DbContext.Add(detail, UserInfo);

                        await DbContext.SaveChangesAsync();

                        // Create product inventory instance
                        var productInventory = new ProductInventory
                        {
                            ProductID = detail.ProductID,
                            LocationID = warehouses.First().ID,
                            OrderDetailID = detail.ID,
                            Quantity = detail.Quantity * -1,
                            CreationUser = header.CreationUser,
                            CreationDateTime = DateTime.Now
                        };

                        // Save product inventory
                        DbContext.Add(productInventory);
                    }

                    await DbContext.SaveChangesAsync();

                    response.Model = header;

                    // Commit transaction
                    transaction.Commit();

                    Logger.LogInformation(SalesDisplays.CreateOrderMessage);
                }
                catch (Exception ex)
                {
                    response.SetError(Logger, nameof(CreateOrderAsync), ex);
                }
            }

            return response;
        }

        public async Task<ISingleResponse<OrderHeader>> CloneOrderAsync(long id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(CloneOrderAsync));

            var response = new SingleResponse<OrderHeader>();

            try
            {
                // Retrieve order by id
                var entity = await DbContext.GetOrderAsync(new OrderHeader(id));

                if (entity == null)
                    return response;

                // Create a new instance for order and set values from existing order
                response.Model = new OrderHeader
                {
                    ID = entity.ID,
                    OrderDate = entity.OrderDate,
                    CustomerID = entity.CustomerID,
                    EmployeeID = entity.EmployeeID,
                    ShipperID = entity.ShipperID,
                    Total = entity.Total,
                    Comments = entity.Comments
                };

                if (entity.OrderDetails?.Count > 0)
                {
                    response.Model.OrderDetails = new Collection<OrderDetail>();

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
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(CloneOrderAsync), ex);
            }

            return response;
        }

        public async Task<IResponse> RemoveOrderAsync(long id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(RemoveOrderAsync));

            var response = new Response();

            try
            {
                // Retrieve order by id
                var entity = await DbContext.GetOrderAsync(new OrderHeader(id));

                if (entity == null)
                    return response;

                // Restrict remove operation for orders with details
                if (entity.OrderDetails.Count > 0)
                    throw new ForeignKeyDependencyException(string.Format(SalesDisplays.RemoveOrderExceptionMessage, id));

                // Delete order
                DbContext.Remove(entity);

                await DbContext.SaveChangesAsync();

                Logger?.LogInformation(SalesDisplays.DeleteOrderMessage);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(RemoveOrderAsync), ex);
            }

            return response;
        }
    }
}
