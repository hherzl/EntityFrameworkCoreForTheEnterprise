using System;

namespace OnlineStore.Core.Domain.Sales
{
    public class OrderInfo
    {
        public OrderInfo()
        {
        }

        public long? OrderID { get; set; }

        public short? OrderStatusID { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public int? ShipperID { get; set; }

        public decimal? Total { get; set; }

        public string CurrencyID { get; set; }

        public Guid? PaymentMethodID { get; set; }

        public int? DetailsCount { get; set; }

        public long? ReferenceOrderID { get; set; }

        public string Comments { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public string CustomerCompanyName { get; set; }

        public string CustomerContactName { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeMiddleName { get; set; }

        public string EmployeeLastName { get; set; }

        public DateTime? EmployeeBirthDate { get; set; }

        public string OrderStatusDescription { get; set; }

        public string ShipperCompanyName { get; set; }

        public string ShipperContactName { get; set; }

        public string CurrencyCurrencyName { get; set; }

        public string CurrencyCurrencySymbol { get; set; }

        public string PaymentMethodPaymentMethodName { get; set; }

        public string PaymentMethodPaymentMethodDescription { get; set; }
    }
}
