using System;

namespace Store.Core.EntityLayer.Dbo
{
    public class Country : IAuditableEntity
    {
        public Country()
        {
        }

        public Int32? CountryID { get; set; }

        public String CountryName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
