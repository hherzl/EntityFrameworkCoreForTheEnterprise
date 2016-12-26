using System;
using Store.Core.EntityLayer.HumanResources;

namespace Store.API.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {

        }

        public EmployeeViewModel(Employee entity)
        {
            EmployeeID = entity.EmployeeID;
            FullName = entity.FirstName + (String.IsNullOrEmpty(entity.MiddleName) ? String.Empty : " " + entity.MiddleName) + " " + entity.LastName;
        }

        public Int32? EmployeeID { get; set; }

        public String FullName { get; set; }
    }
}
