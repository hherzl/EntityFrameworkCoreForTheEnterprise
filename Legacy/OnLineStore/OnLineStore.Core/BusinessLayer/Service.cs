using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.Core.BusinessLayer
{
    public abstract class Service : IService
    {
        protected bool Disposed;
        protected ILogger Logger;
        protected IUserInfo UserInfo;

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
    }
}
