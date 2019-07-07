using System;

namespace OnlineStore.Core.Domain.HumanResources
{
    public class EmployeeAddress : IAuditableEntity
    {
        public EmployeeAddress()
        {
        }

        public int? ID { get; set; }

        public int? EmployeeID { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int? CountryID { get; set; }

        public string PhoneNumber { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
