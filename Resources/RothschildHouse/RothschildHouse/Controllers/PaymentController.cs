using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RothschildHouse.Models;
using RothschildHouse.Requests;

namespace RothschildHouse.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
#pragma warning disable CS1591
        private readonly PaymentDbContext DbContext;

        public PaymentController(PaymentDbContext dbContext)
        {
            DbContext = dbContext;
        }
#pragma warning restore CS1591

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Payment")]
        public async Task<IActionResult> PostPayment([FromBody]PostPaymentRequest request)
        {
            return Ok();
        }
    }
}
