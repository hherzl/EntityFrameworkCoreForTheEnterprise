﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using RothschildHouse.Identity.Domain;

namespace RothschildHouse.Identity.Services
{
    public class RothschildHouseProfileService : IProfileService
    {
        readonly IdentityDbContext DbContext;

        public RothschildHouseProfileService(IdentityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();

                var user = DbContext.GetUserByID(subjectId);

                context.IssuedClaims = DbContext
                    .GetUserClaimsByUserID(user.UserID)
                    .Select(item => new Claim(item.ClaimType, item.ClaimValue))
                    .ToList();
            }
            catch
            {
                // todo: Log exception
            }

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = DbContext.GetUserByID(context.Subject.GetSubjectId());

            context.IsActive = user != null && user.IsActive == true;

            return Task.FromResult(0);
        }
    }
}
