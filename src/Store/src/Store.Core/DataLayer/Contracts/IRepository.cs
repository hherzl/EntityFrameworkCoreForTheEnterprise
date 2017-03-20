using System;
using System.Threading.Tasks;

namespace Store.Core.DataLayer.Contracts
{
    public interface IRepository
    {
        Int32 CommitChanges();

        Task<Int32> CommitChangesAsync();
    }
}
