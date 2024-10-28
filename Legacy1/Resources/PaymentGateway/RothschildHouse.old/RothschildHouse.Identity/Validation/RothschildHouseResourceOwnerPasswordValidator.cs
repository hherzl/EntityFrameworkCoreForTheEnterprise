using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using RothschildHouse.Identity.Domain;

namespace RothschildHouse.Identity.Validation
{
    public class RothschildHouseResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        readonly IdentityDbContext DbContext;

        public RothschildHouseResourceOwnerPasswordValidator(IdentityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (DbContext.ValidatePassword(context.UserName, context.Password))
                context.Result = new GrantValidationResult(DbContext.GetUserByUserName(context.UserName).UserID, "password", null, "local", null);
            else
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The user name and password do not match.", null);

            return Task.FromResult(context.Result);
        }
    }
}
