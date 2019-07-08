using Microsoft.Extensions.Logging;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.Domain;

namespace OnlineStore.Core.BusinessLayer
{
    public abstract class Service : IService
    {
        protected bool Disposed;
        protected readonly ILogger Logger;

        public Service(ILogger logger, OnlineStoreDbContext dbContext, IUserInfo userInfo)
        {
            Logger = logger;
            DbContext = dbContext;
            UserInfo = userInfo;
        }

        public void Dispose()
        {
            if (Disposed)
                return;

            DbContext?.Dispose();

            Disposed = true;
        }

        public OnlineStoreDbContext DbContext { get; }

        public IUserInfo UserInfo { get; set; }
    }
}
