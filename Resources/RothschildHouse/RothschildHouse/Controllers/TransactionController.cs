using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Models;
using RothschildHouse.Requests;

namespace RothschildHouse.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
#pragma warning disable CS1591
        private readonly PaymentDbContext DbContext;

        public TransactionController(PaymentDbContext dbContext)
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
            // todo: check if customer exists in database

            var creditCards = await DbContext.GetCreditCardByCardHolderName(request.CardHolderName).ToListAsync();

            var creditCard = default(CreditCard);

            if (creditCards.Count > 1)
            {
                creditCard = creditCards.FirstOrDefault(item => item.CardNumber == request.CardNumber);

                if (creditCard == null)
                    return BadRequest(string.Format("There isn't record for credit card with last 4 digits: {0}.", request.CardNumber));
            }
            else if (creditCards.Count == 1)
            {
                creditCard = creditCards.First();
            }
            else
            {
                return BadRequest(string.Format("There isn't record for credit card with last 4 digits: {0}.", request.CardNumber));
            }

            // todo: check credit card info in database

            if (!creditCard.IsValid(request))
                return BadRequest(string.Format("There is invalid data for credit card in this transaction."));

            // todo: check if customer has available credit (limit)

            if (!creditCard.HasFounds(request))
                return BadRequest(string.Format("There isn't founds to approve this transaction."));

            var txn = new PaymentTransaction
            {
                PaymentTransactionID = Guid.NewGuid(),
                CreditCardID = creditCard.CreditCardID,
                ConfirmationID = Guid.NewGuid(),
                Amount = request.Amount,
                PaymentDateTime = DateTime.Now
            };

            DbContext.PaymentTransactions.Add(txn);

            creditCard.AvailableFounds -= request.Amount;

            await DbContext.SaveChangesAsync();

            return Ok(new { txn.ConfirmationID, creditCard.Last4Digits });
        }
    }
}
