using System.Net;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.API.PaymentGateway.Models
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static IActionResult ToOkResult(this Response response)
            => new OkObjectResult(response);

        public static IActionResult ToCreatedResult(this Response response)
            => new OkObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
    }
}
