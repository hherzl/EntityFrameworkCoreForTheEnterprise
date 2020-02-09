using System;

namespace RothschildHouse.IdentityServer.Domain
{
    public class UserClaim
    {
        public UserClaim()
        {
        }

        public UserClaim(Guid userClaimID, string userID, string claimType, string claimValue)
        {
            UserClaimID = userClaimID;
            UserID = userID;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public Guid? UserClaimID { get; set; }

        public string UserID { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
