using System.Linq;
using Microsoft.Extensions.Options;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Mapping;
using Store.Core.DataLayer.Repositories;
using Xunit;

namespace Store.Core.Tests
{
    public class ProductionTests
    {
        private IProductionRepository ProductionRepository
        {
            get
            {
                var appSettings = Options.Create(AppSettingsMock.Default);

                var entityMapper = new StoreEntityMapper() as IEntityMapper;

                return new ProductionRepository(new StoreDbContext(appSettings, entityMapper)) as IProductionRepository;
            }
        }

        [Fact]
        public void GetProductsTestAsync()
        {
            var list = ProductionRepository.GetProducts().ToList();
        }
    }
}
