using System;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;

namespace OnLineStore.IdentityServer.Models
{
    public static class AuthDbContextExtentions
    {
        public static bool ValidatePassword(this AuthDbContext dbContext, string userName, string password)
        {
            var user = dbContext.Users.FirstOrDefault(item => item.Email == userName);

            if (user == null)
                return false;

            if (user.Password == password)
                return true;

            return false;
        }

        public static User GetUserByUserName(this AuthDbContext dbContext, string userName)
            => dbContext.Users.FirstOrDefault(item => item.Email == userName);

        public static User GetUserByID(this AuthDbContext dbContext, string id)
            => dbContext.Users.FirstOrDefault(item => item.UserID == id);

        public static IEnumerable<UserClaim> GetUserClaimsByUserID(this AuthDbContext dbContext, string userId)
            => dbContext.UserClaims.Where(item => item.UserID == userId);

        public static void SeedInMemory(this AuthDbContext dbContext)
        {
            dbContext.Users.Add(new User { UserID = "1000", Email = "carloshdez@outlook.com", Password = "password1", IsActive = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "1000" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Administrator" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "carlosfdez" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.GivenName, ClaimValue = "Carlos" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.MiddleName, ClaimValue = "A" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "1000", ClaimType = JwtClaimTypes.FamilyName, ClaimValue = "Hernandez" }
                );

            dbContext.Users.Add(new User { UserID = "20000", Email = "juanperez@gmail.com", Password = "password1", IsActive = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "20000" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Customer" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "juanperez" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.GivenName, ClaimValue = "Juan" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.MiddleName, ClaimValue = "M" },
                new UserClaim { UserClaimID = Guid.NewGuid(), UserID = "20000", ClaimType = JwtClaimTypes.FamilyName, ClaimValue = "Perez" }
            );

            dbContext.SaveChanges();
        }
    }
}
