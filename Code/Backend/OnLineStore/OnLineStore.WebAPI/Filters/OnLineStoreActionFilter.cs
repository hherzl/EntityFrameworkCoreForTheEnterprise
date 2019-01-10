using System;
using IdentityModel;
using Microsoft.AspNetCore.Mvc.Filters;
using OnLineStore.Core;
using OnLineStore.WebAPI.Controllers;

namespace OnLineStore.WebAPI.Filters
{
#pragma warning disable CS1591
    public class OnLineStoreActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as OnLineStoreController;

            controller.UserInfo = new UserInfo();

            foreach (var claim in controller.User.Claims)
            {
                if (claim.Type == JwtClaimTypes.PreferredUserName)
                {
                    controller.UserInfo.Name = claim.Value;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
#pragma warning restore CS1591
}
