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
            dbContext.Users.Add(new User("100", "charlesxavier@gmail.com", "professorx".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Subject, "100"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Id, "45783940-5124-46F9-B54F-5A2149F35117"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Email, "charlesxavier@gmail.com"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.GivenName, "Charles"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.MiddleName, "Francis"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.FamilyName, "Xavier")
            );

            dbContext.Users.Add(new User("200", "erik.lehnsherr@outlook.com", "magneto".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.Subject, "200"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.Id, "F393026E-423A-4A1F-A343-4DB66C1FC8DA"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.Email, "erik.lehnsherr@outlook.com"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.GivenName, "Erik"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.MiddleName, "Magnus"),
                new UserClaim(Guid.NewGuid(), "200", JwtClaimTypes.FamilyName, "Lehnsherr")
            );

            dbContext.SaveChanges();
        }
    }
}
