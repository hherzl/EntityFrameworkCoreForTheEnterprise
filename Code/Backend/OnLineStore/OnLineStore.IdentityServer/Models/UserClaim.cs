using System;

namespace OnLineStore.IdentityServer.Models
{
    public class UserClaim
    {
        public Guid? UserClaimID { get; set; }

        public string UserID { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
