using System;
using System.Linq;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.HumanResources;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public class SalesBusinessObject : BusinessObject, ISalesBusinessObject
    {
        public SalesBusinessObject(UserInfo userInfo, StoreDbContext dbContext)
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
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public IListModelResponse<Employee> GetEmployees(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Employee>() as IListModelResponse<Employee>;

            try
            {
                response.Model = HumanResourcesRepository
                    .GetEmployees(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
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
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public IListModelResponse<Product> GetProducts(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Product>() as IListModelResponse<Product>;

            try
            {
                response.Model = ProductionRepository
                    .GetProducts(pageSize, pageNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
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
                                throw new NonExistingProductException(String.Format("Sent order has a non existing product with ID: '{0}', order has been cancelled.", product.ProductID));
                            }
                            else
                            {
                                detail.ProductName = product.ProductName;
                            }

                            if (product.Discontinued == true)
                            {
                                throw new AddOrderWithDiscontinuedProductException(String.Format("Product with ID: '{0}' is discontinued, order has been cancelled.", product.ProductID));
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

                            var productInventory = new ProductInventory
                            {
                                ProductID = detail.ProductID,
                                EntryDate = DateTime.Now,
                                Quantity = detail.Quantity * -1
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
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
