using System;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Repositories
{
    public abstract class Repository
    {
        protected UserInfo UserInfo;
        protected StoreDbContext DbContext;

        public Repository(UserInfo userInfo, StoreDbContext dbContext)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        protected void Add(IEntity entity)
        {
            var cast = entity as IAuditEntity;

            if (cast != null)
            {
                cast.CreationUser = UserInfo.Name;

                if (!cast.CreationDateTime.HasValue)
                {
                    cast.CreationDateTime = DateTime.Now;
                }
            }
        }

        protected void Update(IEntity entity)
        {
            var cast = entity as IAuditEntity;

            if (cast != null)
            {
                cast.LastUpdateUser = UserInfo.Name;

                if (!cast.LastUpdateDateTime.HasValue)
                {
                    cast.LastUpdateDateTime = DateTime.Now;
                }
            }
        }
    }
}
