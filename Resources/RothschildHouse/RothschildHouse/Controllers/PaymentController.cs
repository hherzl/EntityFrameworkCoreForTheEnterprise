using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Requests;

namespace RothschildHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
        }

        public async Task<IActionResult> PostPayment(PostPaymentRequest request)
        {
            return Ok();
        }
    }
}
