using System.Net;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.API.PaymentGateway.Models
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static IActionResult ToOkResult(this IResponse response)
            => new OkObjectResult(response);

        public static IActionResult ToCreatedResult(this IResponse response)
            => new OkObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
    }
}
