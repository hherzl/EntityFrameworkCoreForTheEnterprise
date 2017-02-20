using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected IQueryable<TEntity> Paging<TEntity>(Int32 pageSize = 0, Int32 pageNumber = 0) where TEntity : class, IEntity
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            return pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
        }

        protected IQueryable<T> Paging<T>(IQueryable<T> query, Int32 pageSize = 0, Int32 pageNumber = 0) where T : class
        {
            return pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
        }

        protected virtual void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
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

            DbContext.Set<TEntity>().Add(entity);
        }

        protected virtual void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
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

        protected virtual void Remove <TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            DbContext.Set<TEntity>().Remove(entity);
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
                            // todo: improve the way to retrieve primary key value from entity instance
                            var key = entry.Entity.GetType().GetProperties()[0].GetValue(entry.Entity, null).ToString();

                            yield return new ChangeLog
                            {
                                ClassName = entityType.Name,
                                PropertyName = property.Name,
                                Key = key,
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

        public Int32 CommitChanges()
        {
            var dbSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbSet.Add(change);
            }

            return DbContext.SaveChanges();
        }

        public Task<Int32> CommitChangesAsync()
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
