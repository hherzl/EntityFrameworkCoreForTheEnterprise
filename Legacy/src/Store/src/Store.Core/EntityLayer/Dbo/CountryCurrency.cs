using System;

namespace Store.Core.EntityLayer.Dbo
{
    public class CountryCurrency : IAuditableEntity
    {
        public CountryCurrency()
        {
        }

        public Int32? CountryCurrencyID { get; set; }

        public Int32? CountryID { get; set; }

        public Int16? CurrencyID { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
