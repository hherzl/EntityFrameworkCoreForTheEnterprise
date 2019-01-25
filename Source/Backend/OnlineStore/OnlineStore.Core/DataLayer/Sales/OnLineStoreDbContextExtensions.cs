using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.EntityLayer.Sales;

namespace OnlineStore.Core.DataLayer.Sales
{
    public static class OnlineStoreDbContextExtensions
    {
        public static async Task<OrderStatus> GetOrderStatusAsync(this OnlineStoreDbContext dbContext, OrderStatus entity)
            => await dbContext.OrderStatuses.FirstOrDefaultAsync(item => item.OrderStatusID == entity.OrderStatusID);

        public static async Task<Customer> GetCustomerAsync(this OnlineStoreDbContext dbContext, Customer entity)
            => await dbContext.Customers.FirstOrDefaultAsync(item => item.CustomerID == entity.CustomerID);

        public static async Task<Shipper> GetShipperAsync(this OnlineStoreDbContext dbContext, Shipper entity)
            => await dbContext.Shippers.FirstOrDefaultAsync(item => item.ShipperID == entity.ShipperID);

        public static IQueryable<OrderInfo> GetOrders(this OnlineStoreDbContext dbContext, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, string currencyID = null, Guid? paymentMethodID = null)
        {
            var query = from order in dbContext.Orders
                        join currencyJoin in dbContext.Currencies on order.CurrencyID equals currencyJoin.CurrencyID into currencyTemp
                        from currency in currencyTemp.DefaultIfEmpty()
                        join customer in dbContext.Customers on order.CustomerID equals customer.CustomerID
                        join employeeJoin in dbContext.Employees on order.EmployeeID equals employeeJoin.EmployeeID into employeeTemp
                        from employee in employeeTemp.DefaultIfEmpty()
                        join orderStatus in dbContext.OrderStatuses on order.OrderStatusID equals orderStatus.OrderStatusID
                        join paymentMethodJoin in dbContext.PaymentMethods on order.PaymentMethodID equals paymentMethodJoin.PaymentMethodID into paymentMethodTemp
                        from paymentMethod in paymentMethodTemp.DefaultIfEmpty()
                        join shipperJoin in dbContext.Shippers on order.ShipperID equals shipperJoin.ShipperID into shipperTemp
                        from shipper in shipperTemp.DefaultIfEmpty()
                        select new OrderInfo
                        {
                            OrderID = order.OrderHeaderID,
                            OrderStatusID = order.OrderStatusID,
                            CustomerID = order.CustomerID,
                            EmployeeID = order.EmployeeID,
                            ShipperID = order.ShipperID,
                            OrderDate = order.OrderDate,
                            Total = order.Total,
                            CurrencyID = order.CurrencyID,
                            PaymentMethodID = order.PaymentMethodID,
                            Comments = order.Comments,
                            DetailsCount = order.DetailsCount,
                            ReferenceOrderID = order.ReferenceOrderID,
                            CreationUser = order.CreationUser,
                            CreationDateTime = order.CreationDateTime,
                            LastUpdateUser = order.LastUpdateUser,
                            LastUpdateDateTime = order.LastUpdateDateTime,
                            Timestamp = order.Timestamp,
                            CurrencyCurrencyName = currency == null ? string.Empty : currency.CurrencyName,
                            CurrencyCurrencySymbol = currency == null ? string.Empty : currency.CurrencySymbol,
                            CustomerCompanyName = customer == null ? string.Empty : customer.CompanyName,
                            CustomerContactName = customer == null ? string.Empty : customer.ContactName,
                            EmployeeFirstName = employee.FirstName,
                            EmployeeMiddleName = employee == null ? string.Empty : employee.MiddleName,
                            EmployeeLastName = employee.LastName,
                            EmployeeBirthDate = employee.BirthDate,
                            OrderStatusDescription = orderStatus.Description,
                            PaymentMethodPaymentMethodName = paymentMethod == null ? string.Empty : paymentMethod.PaymentMethodName,
                            PaymentMethodPaymentMethodDescription = paymentMethod == null ? string.Empty : paymentMethod.PaymentMethodDescription,
                            ShipperCompanyName = shipper == null ? string.Empty : shipper.CompanyName,
                            ShipperContactName = shipper == null ? string.Empty : shipper.ContactName,
                        };

            if (orderStatusID.HasValue)
                query = query.Where(item => item.OrderStatusID == orderStatusID);

            if (customerID.HasValue)
                query = query.Where(item => item.CustomerID == customerID);

            if (employeeID.HasValue)
                query = query.Where(item => item.EmployeeID == employeeID);

            if (shipperID.HasValue)
                query = query.Where(item => item.ShipperID == shipperID);

            if (!string.IsNullOrEmpty(currencyID))
                query = query.Where(item => item.CurrencyID == currencyID);

            if (paymentMethodID.HasValue)
                query = query.Where(item => item.PaymentMethodID == paymentMethodID);

            return query;
        }

        public static async Task<OrderHeader> GetOrderAsync(this OnlineStoreDbContext dbContext, OrderHeader entity)
            => await dbContext.Orders.Include(p => p.OrderDetails).FirstOrDefaultAsync(item => item.OrderHeaderID == entity.OrderHeaderID);
    }
}
