using System;
using Microsoft.Extensions.Logging;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Repositories;

namespace Store.Core.BusinessLayer
{
    public abstract class BusinessObject : IBusinessObject
    {
        protected ILogger Logger;
        protected IUserInfo UserInfo;
        protected Boolean Disposed;
        protected StoreDbContext DbContext;
        protected IHumanResourcesRepository m_humanResourcesRepository;
        protected IProductionRepository m_productionRepository;
        protected ISalesRepository m_salesRepository;

        public BusinessObject(ILogger logger, IUserInfo userInfo, StoreDbContext dbContext)
        {
            Logger = logger;
            UserInfo = userInfo;
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

        protected IHumanResourcesRepository HumanResourcesRepository
        {
            get
            {
                return m_humanResourcesRepository ?? (m_humanResourcesRepository = new HumanResourcesRepository(UserInfo, DbContext));
            }
        }

        protected IProductionRepository ProductionRepository
        {
            get
            {
                return m_productionRepository ?? (m_productionRepository = new ProductionRepository(UserInfo, DbContext));
            }
        }

        protected ISalesRepository SalesRepository
        {
            get
            {
                return m_salesRepository ?? (m_salesRepository = new SalesRepository(UserInfo, DbContext));
            }
        }
    }
}
