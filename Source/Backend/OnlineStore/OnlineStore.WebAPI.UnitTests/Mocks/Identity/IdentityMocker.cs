using System.Security.Claims;
using IdentityModel;

namespace OnlineStore.WebAPI.UnitTests.Mocks.Identity
{
    public static class IdentityMocker
    {
        public static ClaimsPrincipal GetCustomerIdentity()
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtClaimTypes.PreferredUserName, "jameslogan"),
                new Claim(JwtClaimTypes.Email, "jameslogan@walla.com"),
                new Claim(JwtClaimTypes.Role, "Customer"),
                new Claim(JwtClaimTypes.GivenName, "James"),
                new Claim(JwtClaimTypes.MiddleName, ""),
                new Claim(JwtClaimTypes.FamilyName, "Logan")
            }));

        public static ClaimsPrincipal GetWarehouseOperatorIdentity()
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtClaimTypes.PreferredUserName, "warehouseoperator1"),
                new Claim(JwtClaimTypes.Email, "warehouseoperator1@onlinestore.com"),
                new Claim(JwtClaimTypes.Role, "WarehouseOperator")
            }));
    }
}
