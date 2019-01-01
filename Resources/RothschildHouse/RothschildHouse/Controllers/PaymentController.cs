using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Requests;

namespace RothschildHouse.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
        }

        public async Task<IActionResult> PostPayment([FromBody]PostPaymentRequest request)
        {
            return Ok();
        }
    }
}
