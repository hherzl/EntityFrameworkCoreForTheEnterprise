using System;

namespace Store.Core.EntityLayer.Sales
{
    public class PaymentMethod : IAuditableEntity
    {
        public PaymentMethod()
        {
        }

        public Guid? PaymentMethodID { get; set; }

        public String PaymentMethodName { get; set; }

        public String PaymentMethodDescription { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
