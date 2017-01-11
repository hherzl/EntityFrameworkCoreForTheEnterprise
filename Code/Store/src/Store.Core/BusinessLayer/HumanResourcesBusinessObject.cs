using System;
using System.Linq;
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

        public IListModelResponse<Employee> GetEmployees(Int32 pageSize, Int32 pageNumber)
        {
            var response = new ListModelResponse<Employee>() as IListModelResponse<Employee>;

            try
            {
                response.Model = HumanResourcesRepository
                    .GetEmployees(pageSize, pageNumber)
                    .ToList();
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
