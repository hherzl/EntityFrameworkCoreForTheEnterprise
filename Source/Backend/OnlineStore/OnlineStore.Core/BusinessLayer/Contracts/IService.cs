using System;
using OnlineStore.Core.DomainDrivenDesign;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface IService : IDisposable
    {
        OnlineStoreDbContext DbContext { get; }
    }
}
