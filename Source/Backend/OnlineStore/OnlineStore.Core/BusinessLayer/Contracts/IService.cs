using System;
using OnlineStore.Core.DataLayer;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface IService : IDisposable
    {
        OnlineStoreDbContext DbContext { get; }
    }
}
