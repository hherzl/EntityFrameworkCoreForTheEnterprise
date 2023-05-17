using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Dbo;

namespace OnlineStore.Core.Domain
{
    public static class OnlineStoreDbContextExtensions
    {
        public static void Add<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity, IUserInfo userInfo) where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity cast)
            {
                if (string.IsNullOrEmpty(cast.CreationUser))
                    cast.CreationUser = userInfo.UserName;

                if (!cast.CreationDateTime.HasValue)
                    cast.CreationDateTime = DateTime.Now;
            }

            dbContext.Set<TEntity>().Add(entity);
        }

        public static void Update<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity, IUserInfo userInfo) where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity cast)
            {
                if (string.IsNullOrEmpty(cast.LastUpdateUser))
                    cast.LastUpdateUser = userInfo.UserName;

                if (!cast.LastUpdateDateTime.HasValue)
                    cast.LastUpdateDateTime = DateTime.Now;
            }

            dbContext.Set<TEntity>().Update(entity);
        }

        public static void Remove<TEntity>(this OnlineStoreDbContext dbContext, TEntity entity) where TEntity : class, IAuditableEntity
            => dbContext.Set<TEntity>().Remove(entity);

        private static IEnumerable<ChangeLog> GetChanges(this OnlineStoreDbContext dbContext, IUserInfo userInfo)
        {
            var exclusions = dbContext.ChangeLogExclusions.ToList();

            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Modified)
                    continue;

                var entityType = entry.Entity.GetType();

                if (exclusions.Count(item => item.EntityName == entityType.Name && item.PropertyName == "*") == 1)
                    yield break;

                foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                {
                    // Validate if there is an exclusion for *.Property
                    if (exclusions.Count(item => item.EntityName == "*" && string.Compare(item.PropertyName, property.Name, true) == 0) == 1)
                        continue;

                    // Validate if there is an exclusion for Entity.Property
                    if (exclusions.Count(item => item.EntityName == entityType.Name && string.Compare(item.PropertyName, property.Name, true) == 0) == 1)
                        continue;

                    var originalValue = entry.Property(property.Name).OriginalValue;
                    var currentValue = entry.Property(property.Name).CurrentValue;

                    if (string.Concat(originalValue) == string.Concat(currentValue))
                        continue;

                    // todo: improve the way to retrieve primary key value from entity instance

                    var key = entry.Entity.GetType().GetProperties().First().GetValue(entry.Entity, null).ToString();

                    yield return new ChangeLog
                    {
                        ClassName = entityType.Name,
                        PropertyName = property.Name,
                        Key = key,
                        OriginalValue = originalValue == null ? string.Empty : originalValue.ToString(),
                        CurrentValue = currentValue == null ? string.Empty : currentValue.ToString(),
                        UserName = userInfo.UserName,
                        ChangeDate = DateTime.Now
                    };
                }
            }
        }
    }
}
