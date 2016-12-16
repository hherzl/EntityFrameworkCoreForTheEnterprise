using System;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Repositories;

namespace Store.Core.BusinessLayer
{
    public class BusinessObject : IBusinessObject
    {
        protected Boolean Disposed;
        protected UserInfo UserInfo;
        protected StoreDbContext DbContext;
        protected IHumanResourcesRepository m_humanResourcesRepository;
        protected IProductionRepository m_productionRepository;
        protected ISalesRepository m_salesRepository;

        public BusinessObject(UserInfo userInfo, StoreDbContext dbContext)
        {
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
