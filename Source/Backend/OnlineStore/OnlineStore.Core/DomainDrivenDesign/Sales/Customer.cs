using System;
using System.Collections.ObjectModel;

namespace OnlineStore.Core.Domain.Sales
{
    public class Customer : IAuditableEntity
    {
        public Customer()
        {
        }

        public Customer(int? id)
        {
            ID = id;
        }

        public int? ID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual Collection<OrderHeader> Orders { get; set; }
    }
}
