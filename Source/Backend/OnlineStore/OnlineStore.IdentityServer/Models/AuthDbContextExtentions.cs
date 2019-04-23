using System;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;

namespace OnlineStore.IdentityServer.Models
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

        public static IEnumerable<UserClaim> GetUserClaimsByUserID(this AuthDbContext dbContext, string userID)
            => dbContext.UserClaims.Where(item => item.UserID == userID);

        public static void SeedInMemory(this AuthDbContext dbContext)
        {
            dbContext.Users.Add(new User("1000", "erik.lehnsherr@outlook.com", "magneto".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.Subject, "1000"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.PreferredUserName, "eriklehnsherr"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.Role, "Administrator"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.Email, "erik.lehnsherr@outlook.com"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.GivenName, "Erik"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.MiddleName, "M"),
                new UserClaim(Guid.NewGuid(), "1000", JwtClaimTypes.FamilyName, "Lehnsherr")
            );

            dbContext.Users.Add(new User("2000", "charlesxavier@gmail.com", "professorx".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.Subject, "2000"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.PreferredUserName, "charlesxavier"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.Role, "Administrator"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.Email, "charlesxavier@gmail.com"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.GivenName, "Charles"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.MiddleName, "F"),
                new UserClaim(Guid.NewGuid(), "2000", JwtClaimTypes.FamilyName, "Xavier")
            );

            dbContext.Users.Add(new User("3000", "jameslogan@walla.com", "wolverine".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.Subject, "3000"),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.PreferredUserName, "jameslogan"),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.Email, "jameslogan@walla.com"),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.GivenName, "James"),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.MiddleName, ""),
                new UserClaim(Guid.NewGuid(), "3000", JwtClaimTypes.FamilyName, "Logan")
            );

            dbContext.Users.Add(new User("4000", "ororo_munroe@yahoo.com", "storm".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.Subject, "4000"),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.PreferredUserName, "ororo_munroe"),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.Role, "Customer"),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.Email, "ororo_munroe@yahoo.com"),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.GivenName, "Ororo"),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.MiddleName, ""),
                new UserClaim(Guid.NewGuid(), "4000", JwtClaimTypes.FamilyName, "Munroe")
            );

            dbContext.Users.Add(new User("5000", "warehousemanager1@onlinestore.com", "password1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "5000", JwtClaimTypes.Subject, "5000"),
                new UserClaim(Guid.NewGuid(), "5000", JwtClaimTypes.PreferredUserName, "warehousemanager1"),
                new UserClaim(Guid.NewGuid(), "5000", JwtClaimTypes.Role, "WarehouseManager"),
                new UserClaim(Guid.NewGuid(), "5000", JwtClaimTypes.Email, "warehousemanager1@onlinestore.com")
            );

            dbContext.Users.Add(new User("6000", "warehouseoperator1@onlinestore.com", "password1".ToSha256(), true));

            dbContext.UserClaims.AddRange(
                new UserClaim(Guid.NewGuid(), "6000", JwtClaimTypes.Subject, "6000"),
                new UserClaim(Guid.NewGuid(), "6000", JwtClaimTypes.PreferredUserName, "warehouseoperator1"),
                new UserClaim(Guid.NewGuid(), "6000", JwtClaimTypes.Role, "WarehouseOperator"),
                new UserClaim(Guid.NewGuid(), "6000", JwtClaimTypes.Email, "warehouseoperator1@onlinestore.com")
            );

            dbContext.SaveChanges();
        }
    }
}
