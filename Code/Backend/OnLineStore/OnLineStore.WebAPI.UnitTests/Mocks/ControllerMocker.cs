using OnlineStore.Common;
using OnlineStore.WebAPI.Controllers;

namespace OnlineStore.WebAPI.UnitTests.Mocks
{
    public static class ControllerMocker
    {
        public static SalesController GetSalesController(string name)
        {
            var logger = LoggingHelper.GetLogger<SalesController>();
            var paymentClient = new MockedRothschildHouseClient();
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, name);

            return new SalesController(logger, paymentClient, service);
        }
    }
}
