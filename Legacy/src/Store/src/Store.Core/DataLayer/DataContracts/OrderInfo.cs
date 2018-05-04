using System;

namespace Store.Core.DataLayer.DataContracts
{
    public class OrderInfo
    {
        public OrderInfo()
        {
        }

        public Int64? OrderID { get; set; }

        public Int16? OrderStatusID { get; set; }

        public DateTime? OrderDate { get; set; }

        public Int32? CustomerID { get; set; }

        public Int32? EmployeeID { get; set; }

        public Int32? ShipperID { get; set; }

        public Decimal? Total { get; set; }

        public Int16? CurrencyID { get; set; }

        public Guid? PaymentMethodID { get; set; }

        public String Comments { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public String CustomerCompanyName { get; set; }

        public String CustomerContactName { get; set; }

        public String EmployeeFirstName { get; set; }

        public String EmployeeMiddleName { get; set; }

        public String EmployeeLastName { get; set; }

        public DateTime? EmployeeBirthDate { get; set; }

        public String OrderStatusDescription { get; set; }

        public String ShipperCompanyName { get; set; }

        public String ShipperContactName { get; set; }

        public String CurrencyCurrencyName { get; set; }

        public String CurrencyCurrencySymbol { get; set; }

        public String PaymentMethodPaymentMethodName { get; set; }

        public String PaymentMethodPaymentMethodDescription { get; set; }
    }
}
