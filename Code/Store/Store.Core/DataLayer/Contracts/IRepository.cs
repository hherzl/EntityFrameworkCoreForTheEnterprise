using System.Threading.Tasks;

namespace Store.Core.DataLayer.Contracts
{
    public interface IRepository
    {
        int CommitChanges();

        Task<int> CommitChangesAsync();
    }
}
