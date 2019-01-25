using System;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.Core.BusinessLayer.Contracts
{
    public interface IService : IDisposable
    {
        OnLineStoreDbContext DbContext { get; }
    }
}
