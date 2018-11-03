using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.DataLayer;
using OnLineStore.Core.DataLayer.Contracts;
using OnLineStore.Core.DataLayer.Repositories;

namespace OnLineStore.Core.BusinessLayer
{
    public abstract class Service : IService
    {
        protected ILogger Logger;
        protected IUserInfo UserInfo;
        protected bool Disposed;
        protected readonly StoreDbContext DbContext;
        protected IHumanResourcesRepository m_humanResourcesRepository;
        protected IProductionRepository m_productionRepository;
        protected ISalesRepository m_salesRepository;

        public Service(ILogger logger, IUserInfo userInfo, StoreDbContext dbContext)
        {
            Logger = logger;
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                DbContext?.Dispose();

                Disposed = true;
            }
        }

        protected IHumanResourcesRepository HumanResourcesRepository
            => m_humanResourcesRepository ?? (m_humanResourcesRepository = new HumanResourcesRepository(UserInfo, DbContext));

        protected IProductionRepository ProductionRepository
            => m_productionRepository ?? (m_productionRepository = new ProductionRepository(UserInfo, DbContext));

        protected ISalesRepository SalesRepository
            => m_salesRepository ?? (m_salesRepository = new SalesRepository(UserInfo, DbContext));
    }
}
