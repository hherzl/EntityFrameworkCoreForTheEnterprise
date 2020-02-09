using System;
using OnlineStore.Core.Domain;

namespace OnlineStore.Core.Business.Contracts
{
    public interface IService : IDisposable
    {
        OnlineStoreDbContext DbContext { get; }

        IUserInfo UserInfo { get; set; }
    }
}
