using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IHumanResourcesBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Employee>> GetEmployeesAsync(Int32 pageSize = 0, Int32 pageNumber = 0);

        Task<ISingleModelResponse<Employee>> UpdateEmployeeAsync(Employee changes);
    }
}
