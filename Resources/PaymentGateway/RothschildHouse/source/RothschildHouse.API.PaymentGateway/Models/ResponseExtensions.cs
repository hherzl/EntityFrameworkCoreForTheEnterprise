using System.Net;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Library.Client.DataContracts.Common.Contracts;

namespace RothschildHouse.API.PaymentGateway.Models
{
    public static class ResponseExtensions
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
