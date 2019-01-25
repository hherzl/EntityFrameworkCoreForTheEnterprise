using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnLineStore.Core.EntityLayer.HumanResources;

namespace OnLineStore.Core.DataLayer.HumanResources
{
    public static class OnLineStoreDbContextExtensions
    {
        public static async Task<Employee> GetEmployeeAsync(this OnLineStoreDbContext dbContext, Employee entity)
            => await dbContext.Employees.FirstOrDefaultAsync(item => item.EmployeeID == entity.EmployeeID);
    }
}
