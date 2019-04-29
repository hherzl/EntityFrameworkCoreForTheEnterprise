using OnlineStore.Common.Helpers;
using OnlineStore.WebAPI.Controllers;
using OnlineStore.WebAPI.UnitTests.Mocks.Identity;
using OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway;

namespace OnlineStore.WebAPI.UnitTests.Mocks
{
    public static class ControllerMocker
    {
        public static SalesController GetSalesController(string name)
        {
            var logger = LoggingHelper.GetLogger<SalesController>();
            var identityClient = new MockedRothschildHouseIdentityClient();
            var paymentClient = new MockedRothschildHousePaymentClient();
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, name);

            return new SalesController(logger, identityClient, paymentClient, service);
        }
    }
}
