using System;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.Repositories;

namespace Store.Core.BusinessLayer
{
    public class BusinessObject : IBusinessObject
    {
        protected Boolean Disposed;
        protected StoreDbContext DbContext;
        protected IProductionRepository m_productionRepository;
        protected ISalesRepository m_salesRepository;

        public BusinessObject(StoreDbContext dbContext)
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

        protected IProductionRepository ProductionRepository
        {
            get
            {
                return m_productionRepository ?? (m_productionRepository = new ProductionRepository(DbContext));
            }
        }

        protected ISalesRepository SalesRepository
        {
            get
            {
                return m_salesRepository ?? (m_salesRepository = new SalesRepository(DbContext));
            }
        }
    }
}
