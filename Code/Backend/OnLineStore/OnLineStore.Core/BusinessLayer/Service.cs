using Microsoft.Extensions.Logging;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.DataLayer;

namespace OnlineStore.Core.BusinessLayer
{
    public abstract class Service : IService
    {
        protected bool Disposed;
        protected ILogger Logger;
        protected IUserInfo UserInfo;

        public Service(ILogger logger, IUserInfo userInfo, OnlineStoreDbContext dbContext)
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

        public OnlineStoreDbContext DbContext { get; }
    }
}
