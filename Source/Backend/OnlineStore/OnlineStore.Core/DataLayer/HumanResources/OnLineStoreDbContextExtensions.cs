using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.EntityLayer.HumanResources;

namespace OnlineStore.Core.DataLayer.HumanResources
{
    public static class OnlineStoreDbContextExtensions
    {
        public static async Task<Employee> GetEmployeeAsync(this OnlineStoreDbContext dbContext, Employee entity)
            => await dbContext.Employees.FirstOrDefaultAsync(item => item.EmployeeID == entity.EmployeeID);
    }
}
