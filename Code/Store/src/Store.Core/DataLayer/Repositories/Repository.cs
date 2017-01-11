using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Repositories
{
    public abstract class Repository
    {
        protected IUserInfo UserInfo;
        protected StoreDbContext DbContext;

        public Repository(IUserInfo userInfo, StoreDbContext dbContext)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        protected virtual void Add(IEntity entity)
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

        protected virtual void Update(IEntity entity)
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

        protected virtual IEnumerable<ChangeLog> GetChanges()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    var entityType = entry.Entity.GetType();

                    foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                    {
                        var originalValue = entry.Property(property.Name).OriginalValue;
                        var currentValue = entry.Property(property.Name).CurrentValue;

                        if (String.Concat(originalValue) != String.Concat(currentValue))
                        {
                            yield return new ChangeLog
                            {
                                ClassName = entityType.Name,
                                PropertyName = property.Name,
                                OriginalValue = originalValue == null ? String.Empty : originalValue.ToString(),
                                CurrentValue = currentValue == null ? String.Empty : currentValue.ToString(),
                                UserName = UserInfo.Name,
                                ChangeDate = DateTime.Now
                            };
                        }
                    }
                }
            }
        }

        protected void CommitChanges()
        {
            var dbSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbSet.Add(change);
            }

            DbContext.SaveChanges();
        }
    }
}
