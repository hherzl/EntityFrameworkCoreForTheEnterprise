using System.Threading.Tasks;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Contracts
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
