using System;

namespace OnlineStore.Core.EntityLayer.Sales
{
    public class PaymentMethod : IAuditableEntity
    {
        public PaymentMethod()
        {
        }

        public Guid? PaymentMethodID { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDescription { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
