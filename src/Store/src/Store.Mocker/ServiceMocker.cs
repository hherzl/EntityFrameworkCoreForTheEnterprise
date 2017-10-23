using Microsoft.EntityFrameworkCore;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;

namespace Store.Mocker
{
    public class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService()
        {
            var logger = LoggerMocker.GetLogger<IHumanResourcesService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer("server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;")
                .Options;

            return new HumanResourcesService(logger, new UserInfo { Name = "mocker" }, new StoreDbContext(options, new StoreEntityMapper()));
        }

        public static IProductionService GetProductionService()
        {
            var logger = LoggerMocker.GetLogger<IProductionService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer("server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;")
                .Options;

            return new ProductionService(logger, new UserInfo { Name = "mocker" }, new StoreDbContext(options, new StoreEntityMapper()));
        }

        public static ISalesService GetSalesService()
        {
            var logger = LoggerMocker.GetLogger<ISalesService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer("server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;")
                .Options;

            return new SalesService(logger, new UserInfo { Name = "mocker" }, new StoreDbContext(options, new StoreEntityMapper()));
        }
    }
}
