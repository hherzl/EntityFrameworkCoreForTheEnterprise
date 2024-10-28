using System.Security.Claims;
using IdentityModel;

namespace OnlineStore.API.Warehouse.UnitTests.Mocks
{
    public static class IdentityMocker
    {
        public static ClaimsPrincipal GetWarehouseOperatorIdentity()
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtClaimTypes.PreferredUserName, "warehouseoperator1"),
                new Claim(JwtClaimTypes.Email, "warehouseoperator1@onlinestore.com"),
                new Claim(JwtClaimTypes.Role, "WarehouseOperator")
            }));
    }
}
