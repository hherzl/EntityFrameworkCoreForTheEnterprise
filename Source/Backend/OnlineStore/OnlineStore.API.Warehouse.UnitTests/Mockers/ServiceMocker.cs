using System;
using System.Collections.Generic;
using System.Text;
using OnlineStore.Common.Helpers;
using OnlineStore.Core;
using OnlineStore.Core.BusinessLayer;
using OnlineStore.Core.BusinessLayer.Contracts;

namespace OnlineStore.API.Warehouse.UnitTests.Mockers
{
    public static class ServiceMocker
    {
        public static IWarehouseService GetWarehouseService(IUserInfo userInfo, string dbName, bool seedWarehouseSchema = false)
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName, seedWarehouseSchema),
                userInfo
            );
    }
}
