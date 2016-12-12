using System.Linq;
using Microsoft.Extensions.Options;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Mapping;
using Store.Core.DataLayer.Repositories;
using Xunit;

namespace Store.Core.Tests
{
    public class HumanResourcesTests
    {
        private IHumanResourcesRepository HumanResourcesRepository
        {
            get
            {
                var appSettings = Options.Create(AppSettingsMock.Default);

                var entityMapper = new StoreEntityMapper() as IEntityMapper;

                return new HumanResourcesRepository(new StoreDbContext(appSettings, entityMapper)) as IHumanResourcesRepository;
            }
        }

        [Fact]
        public void GetEmployeesTestAsync()
        {
            var list = HumanResourcesRepository.GetEmployees().ToList();
        }
    }
}
