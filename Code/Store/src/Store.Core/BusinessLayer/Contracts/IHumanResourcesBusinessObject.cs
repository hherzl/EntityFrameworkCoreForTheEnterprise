using System;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface IHumanResourcesBusinessObject : IBusinessObject
    {
        IListModelResponse<Employee> GetEmployees(Int32 pageSize, Int32 pageNumber);
    }
}
