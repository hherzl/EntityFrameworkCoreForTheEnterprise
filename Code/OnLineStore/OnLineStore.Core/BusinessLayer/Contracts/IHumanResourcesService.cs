using System.Threading.Tasks;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.EntityLayer.HumanResources;

namespace OnLineStore.Core.BusinessLayer.Contracts
{
    public interface IHumanResourcesService : IService
    {
        Task<IListResponse<Employee>> GetEmployeesAsync(int pageSize = 0, int pageNumber = 0);

        Task<ISingleResponse<Employee>> GetEmployeeAsync(Employee entity);

        Task<ISingleResponse<Employee>> UpdateEmployeeAsync(Employee changes);
    }
}
