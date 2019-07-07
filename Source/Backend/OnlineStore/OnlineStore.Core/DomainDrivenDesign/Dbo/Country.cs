using System;

namespace OnlineStore.Core.Domain.Dbo
{
    public class Country : IAuditableEntity
    {
        public Country()
        {
        }

        public int? ID { get; set; }

        public string  CountryName { get; set; }

        public string  CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string  LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
