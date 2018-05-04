using System;

namespace Store.Core.EntityLayer.Dbo
{
    public class Currency : IAuditableEntity
    {
        public Currency()
        {
        }

        public Int16? CurrencyID { get; set; }

        public String CurrencyName { get; set; }

        public String CurrencySymbol { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
