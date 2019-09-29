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
                new ApiResource("OnlineStoreWarehouseAPI", "Online Store Warehouse API"),
                new ApiResource("OnlineStoreSalesAPI", "Online Store Sales API")
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "onlinestorewarehouseapiclient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("onlinestorewarehouseapiclient1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "OnlineStoreWarehouseAPI"
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
