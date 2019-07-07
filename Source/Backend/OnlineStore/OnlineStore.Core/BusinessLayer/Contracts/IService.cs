using System;
using OnlineStore.Core.Domain;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface IService : IDisposable
    {
        OnlineStoreDbContext DbContext { get; }
    }
}
