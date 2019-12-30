using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

namespace OnlineStore.API.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("OnlineStoreAPI", "Online Store API")
                {
                    ApiSecrets =
                    {
                        new Secret("Secret1")
                    }
                }
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "OnlineStoreAPI.Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("OnlineStoreAPIClientSecret1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "OnlineStoreAPI"
                    },
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Role, "Administrator"),
                        new Claim(JwtClaimTypes.Role, "Customer"),
                        new Claim(JwtClaimTypes.Role, "WarehouseManager"),
                        new Claim(JwtClaimTypes.Role, "WarehouseOperator")
                    }
                }
            };
    }
}
