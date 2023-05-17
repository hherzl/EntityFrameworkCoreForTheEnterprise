using System;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;

namespace RothschildHouse.Identity.Domain
{
    public static class IdentityDbContextExtensions
    {
        public static bool ValidatePassword(this IdentityDbContext dbContext, string userName, string password)
        {
            var user = dbContext.Users.FirstOrDefault(item => item.Email == userName);

            if (user == null)
                return false;

            if (user.Password == password.ToSha256())
                return true;

            return false;
        }

        public static User GetUserByUserName(this IdentityDbContext dbContext, string userName)
            => dbContext.Users.FirstOrDefault(item => item.Email == userName);

        public static User GetUserByID(this IdentityDbContext dbContext, string id)
            => dbContext.Users.FirstOrDefault(item => item.UserID == id);

        public static IEnumerable<UserClaim> GetUserClaimsByUserID(this IdentityDbContext dbContext, string userID)
            => dbContext.UserClaims.Where(item => item.UserID == userID);

        public static void SeedInMemory(this IdentityDbContext dbContext)
        {
            dbContext.Users.Add(new User("100", "administrator@onlinestore.com", "onlinestore1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Subject, "100"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "100", JwtClaimTypes.Email, "administrator@onlinestore.com")
            );

            dbContext.SaveChanges();
        }
    }
}
