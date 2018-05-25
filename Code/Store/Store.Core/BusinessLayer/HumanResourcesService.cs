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
    public class HumanResourcesService : Service, IHumanResourcesService
    {
        public HumanResourcesService(ILogger<HumanResourcesService> logger, IUserInfo userInfo, StoreDbContext dbContext)
            : base(logger, userInfo, dbContext)
        {
        }

        public async Task<IListResponse<Employee>> GetEmployeesAsync(int pageSize = 0, int pageNumber = 0)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetEmployeesAsync));

            var response = new ListResponse<Employee>();

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

        public async Task<ISingleResponse<Employee>> GetEmployeeAsync(Employee entity)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(GetEmployeeAsync));

            var response = new SingleResponse<Employee>();

            try
            {
                response.Model = await HumanResourcesRepository.GetEmployeeAsync(entity);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public async Task<ISingleResponse<Employee>> UpdateEmployeeAsync(Employee changes)
        {
            Logger?.LogInformation("{0} has been invoked", nameof(UpdateEmployeeAsync));

            var response = new SingleResponse<Employee>();

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
