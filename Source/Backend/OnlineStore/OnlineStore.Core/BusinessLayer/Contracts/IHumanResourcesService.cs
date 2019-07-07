using System.Threading.Tasks;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.Domain.HumanResources;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface IHumanResourcesService : IService
    {
        Task<IListResponse<Employee>> GetEmployeesAsync(int pageSize = 0, int pageNumber = 0);

        Task<ISingleResponse<Employee>> GetEmployeeAsync(Employee entity);

        Task<IResponse> UpdateEmployeeAsync(Employee changes);
    }
}
