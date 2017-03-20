using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IHumanResourcesBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Employee>> GetEmployeesAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<ISingleModelResponse<Employee>> UpdateEmployeeAsync(Employee changes);
    }
}
