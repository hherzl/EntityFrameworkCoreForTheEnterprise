using System.Security.Claims;

namespace OnLineStore.WebAPI.UnitTests.Mocks
{
    public static class IdentityMocker
    {
        public static ClaimsPrincipal GetCustomerIdentity()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("preferred_username", "jameslogan"),
                new Claim("email", "jameslogan@walla.com"),
                new Claim("role", "Customer"),
                new Claim("given_name", "James"),
                new Claim("middle_name", ""),
                new Claim("family_name", "Logan")
            }));
        }
    }
}
