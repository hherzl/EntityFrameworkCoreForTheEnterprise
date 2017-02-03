using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.BusinessLayer
{
    public class HumanResourcesBusinessObject : BusinessObject, IHumanResourcesBusinessObject
    {
        public HumanResourcesBusinessObject(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<IListModelResponse<Employee>> GetEmployeesAsync(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Employee>() as IListModelResponse<Employee>;

            try
            {
                response.Model = await HumanResourcesRepository.GetEmployees(pageSize, pageNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public ISingleModelResponse<Employee> UpdateEmployee(Employee changes)
        {
            var response = new SingleModelResponse<Employee>() as ISingleModelResponse<Employee>;

            try
            {
                HumanResourcesRepository.UpdateEmployee(changes);

                response.Model = changes;
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }
    }
}
