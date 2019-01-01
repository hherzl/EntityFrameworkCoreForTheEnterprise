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
            dbContext.Users.Add(new User("1000", "carloshdez@outlook.com", "password1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.Subject, "1000"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.Role, "Administrator"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.PreferredUserName, "carlosfdez"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.GivenName, "Carlos"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.MiddleName, "A"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.FamilyName, "Hernandez")
                );

            dbContext.Users.Add(new User("2000", "juanperez@gmail.com", "password1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.Subject, "2000"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.PreferredUserName, "juanperez"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.GivenName, "Juan"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.MiddleName, "M"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.FamilyName, "Perez")
            );

            dbContext.SaveChanges();
        }
    }
}
