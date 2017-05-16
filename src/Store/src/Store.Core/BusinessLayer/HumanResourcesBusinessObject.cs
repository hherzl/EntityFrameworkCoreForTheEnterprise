using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.BusinessLayer
{
    public class HumanResourcesBusinessObject : BusinessObject, IHumanResourcesBusinessObject
    {
        public HumanResourcesBusinessObject(ILogger logger, IUserInfo userInfo, StoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IListModelResponse<Employee>> GetEmployeesAsync(Int32 pageSize, Int32 pageNumber)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetEmployeesAsync));

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

        public async Task<ISingleModelResponse<Employee>> UpdateEmployeeAsync(Employee changes)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(UpdateEmployeeAsync));

            var response = new SingleModelResponse<Employee>() as ISingleModelResponse<Employee>;

            try
            {
                await HumanResourcesRepository.UpdateEmployeeAsync(changes);

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
