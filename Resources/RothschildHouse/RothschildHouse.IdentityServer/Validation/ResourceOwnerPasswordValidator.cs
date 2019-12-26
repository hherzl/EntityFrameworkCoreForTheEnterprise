using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using RothschildHouse.IdentityServer.Domain;

namespace RothschildHouse.IdentityServer.Validation
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly AuthDbContext DbContext;

        public ResourceOwnerPasswordValidator(AuthDbContext dbContext)
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
