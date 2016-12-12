using System;

namespace Store.Core.DataLayer.Repositories
{
    public abstract class Repository : IDisposable
    {
        protected Boolean Disposed;
        protected StoreDbContext DbContext;

        public Repository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();

                    Disposed = true;
                }
            }
        }
    }
}
