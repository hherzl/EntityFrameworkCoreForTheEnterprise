using System;

namespace OnlineStore.Core.EntityLayer.HumanResources
{
    public class EmployeeEmail : IAuditableEntity
    {
        public EmployeeEmail()
        {
        }

        public int? EmployeeEmailID { get; set; }

        public int? EmployeeID { get; set; }

        public string Email { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
