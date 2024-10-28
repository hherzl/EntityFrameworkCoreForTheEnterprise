using System.Net;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Extensions
{
#pragma warning disable CS1591
    public static class ResponseExtensions
    {
        public static IActionResult ToOkResult(this Response response)
            => new OkObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

        public static IActionResult ToCreatedResult<TResult>(this CreatedResponse<TResult> response)
            => new OkObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
    }
}
