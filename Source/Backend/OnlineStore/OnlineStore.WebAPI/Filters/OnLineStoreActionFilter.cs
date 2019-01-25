using System;
using IdentityModel;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.WebAPI.Controllers;

namespace OnlineStore.WebAPI.Filters
{
#pragma warning disable CS1591
    public class OnlineStoreActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as OnlineStoreController;

            foreach (var claim in controller.User.Claims)
            {
                if (claim.Type == JwtClaimTypes.Email)
                    controller.UserInfo.Email = claim.Value;
                else if (claim.Type == JwtClaimTypes.PreferredUserName)
                    controller.UserInfo.UserName = claim.Value;
                else if (claim.Type == JwtClaimTypes.Role)
                    controller.UserInfo.Role = claim.Value;
                else if (claim.Type == JwtClaimTypes.GivenName)
                    controller.UserInfo.GivenName = claim.Value;
                else if (claim.Type == JwtClaimTypes.MiddleName)
                    controller.UserInfo.MiddleName = claim.Value;
                else if (claim.Type == JwtClaimTypes.FamilyName)
                    controller.UserInfo.FamilyName = claim.Value;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
#pragma warning restore CS1591
}
