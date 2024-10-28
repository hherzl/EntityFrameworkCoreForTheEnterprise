using System;

namespace OnlineStore.Core.Domain.Warehouse
{
    public class ProductUnitPriceHistory : IAuditableEntity
    {
        public ProductUnitPriceHistory()
        {
        }

        public ProductUnitPriceHistory(int? id)
        {
            ID = id;
        }

        public int? ID { get; set; }

        public int? ProductID { get; set; }

        public decimal? UnitPrice { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
