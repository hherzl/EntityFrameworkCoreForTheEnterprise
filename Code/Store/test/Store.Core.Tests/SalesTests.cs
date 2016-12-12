using System.Linq;
using Microsoft.Extensions.Options;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Mapping;
using Store.Core.DataLayer.Repositories;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesTests
    {
        private ISalesRepository SalesRepository
        {
            get
            {
                var appSettings = Options.Create(AppSettingsMock.Default);

                var entityMapper = new StoreEntityMapper() as IEntityMapper;

                return new SalesRepository(new StoreDbContext(appSettings, entityMapper)) as ISalesRepository;
            }
        }

        [Fact]
        public void GetOrdersTestAsync()
        {
            var list = SalesRepository.GetOrders().ToList();
        }
    }
}
