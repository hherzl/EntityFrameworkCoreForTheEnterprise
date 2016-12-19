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
        public HumanResourcesBusinessObject(UserInfo userInfo, StoreDbContext dbContext)
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
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
