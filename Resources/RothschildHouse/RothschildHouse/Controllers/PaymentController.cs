using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Models;
using RothschildHouse.Requests;

namespace RothschildHouse.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private PaymentDbContext DbContext;

        public PaymentController(PaymentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IActionResult> PostPayment([FromBody]PostPaymentRequest request)
        {
            return Ok();
        }
    }
}
