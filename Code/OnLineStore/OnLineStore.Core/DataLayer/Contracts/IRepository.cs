using System.Threading.Tasks;
using OnLineStore.Core.EntityLayer;

namespace OnLineStore.Core.DataLayer.Contracts
{
    public interface IRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity;

        void Update<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity;

        void Remove<TEntity>(TEntity entity) where TEntity : class, IAuditableEntity;

        int CommitChanges();

        Task<int> CommitChangesAsync();
    }
}
