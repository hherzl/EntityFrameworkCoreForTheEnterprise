using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

namespace OnlineStore.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("OnlineStoreWebAPI", "Online Store Web API")
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "onlinestoreclient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("onlinestoreclientsecret1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "OnlineStoreWebAPI"
                    },
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Role, "Administrator"),
                        new Claim(JwtClaimTypes.Role, "Customer")
                    }
                }
            };
    }
}
