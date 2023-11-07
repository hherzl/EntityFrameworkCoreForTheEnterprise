using Microsoft.AspNetCore.Mvc;

namespace RothschildHouse.API.PaymentGateway.Controllers;

[ApiController]
[Route("api/v1")]
public class PaymentGatewayController : ControllerBase
{
    public PaymentGatewayController()
    {
    }

    [HttpGet("foo")]
    public string Foo()
        => "Foo bar baz";
}
