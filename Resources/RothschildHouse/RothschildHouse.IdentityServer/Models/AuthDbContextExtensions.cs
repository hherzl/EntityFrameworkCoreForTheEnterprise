using System;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;

namespace RothschildHouse.IdentityServer.Models
{
    public static class AuthDbContextExtensions
    {
        public static bool ValidatePassword(this AuthDbContext dbContext, string userName, string password)
        {
            var user = dbContext.Users.FirstOrDefault(item => item.Email == userName);

            if (user == null)
                return false;

            if (user.Password == password.ToSha256())
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
            dbContext.Users.Add(new User("100", "charlesx@gmail.com", "password1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Subject, "100"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.PreferredUserName, "charlesxavier"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.GivenName, "Charles"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.MiddleName, "F"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.FamilyName, "Xavier")
            );

            dbContext.SaveChanges();
        }
    }
}
