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
        protected IHumanResourcesRepository m_humanResourcesRepository;

        public Service(ILogger logger, IUserInfo userInfo, OnLineStoreDbContext dbContext)
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

        public OnLineStoreDbContext DbContext { get; }

        protected IHumanResourcesRepository HumanResourcesRepository
            => m_humanResourcesRepository ?? (m_humanResourcesRepository = new HumanResourcesRepository(UserInfo, DbContext));
    }
}
