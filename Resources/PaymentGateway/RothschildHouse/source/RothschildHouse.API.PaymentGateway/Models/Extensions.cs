using System.Net;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Application.Common;

namespace RothschildHouse.API.PaymentGateway.Models;

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
