﻿using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Person : AuditableEntity
    {
        public Person()
        {
        }

        public int? Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }

        public virtual Collection<Customer> CustomerList { get; set; }
    }
}
