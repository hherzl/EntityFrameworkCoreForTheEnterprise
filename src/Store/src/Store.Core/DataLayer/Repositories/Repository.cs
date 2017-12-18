using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Repositories
{
    public abstract class Repository
    {
        protected IUserInfo UserInfo;
        protected DbContext DbContext;

        public Repository(IUserInfo userInfo, DbContext dbContext)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        protected virtual void Add<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity
        {
            var cast = entity as IAuditableEntity;

            if (cast != null)
            {
                cast.CreationUser = UserInfo.Name;

                if (!cast.CreationDateTime.HasValue)
                {
                    cast.CreationDateTime = DateTime.Now;
                }
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        protected virtual void Update<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity
        {
            var cast = entity as IAuditableEntity;

            if (cast != null)
            {
                cast.LastUpdateUser = UserInfo.Name;

                if (!cast.LastUpdateDateTime.HasValue)
                {
                    cast.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        protected virtual void Remove<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity
            => DbContext.Set<TEntity>().Remove(entity);

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

                        if (string.Concat(originalValue) != string.Concat(currentValue))
                        {
                            // todo: improve the way to retrieve primary key value from entity instance
                            var key = entry.Entity.GetType().GetProperties()[0].GetValue(entry.Entity, null).ToString();

                            yield return new ChangeLog
                            {
                                ClassName = entityType.Name,
                                PropertyName = property.Name,
                                Key = key,
                                OriginalValue = originalValue == null ? string.Empty : originalValue.ToString(),
                                CurrentValue = currentValue == null ? string.Empty : currentValue.ToString(),
                                UserName = UserInfo.Name,
                                ChangeDate = DateTime.Now
                            };
                        }
                    }
                }
            }
        }

        public int CommitChanges()
        {
            var dbSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbSet.Add(change);
            }

            return DbContext.SaveChanges();
        }

        public Task<int> CommitChangesAsync()
        {
            var dbSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbSet.Add(change);
            }

            return DbContext.SaveChangesAsync();
        }
    }
}
