using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Models;
using RothschildHouse.Requests;
using RothschildHouse.Responses;

namespace RothschildHouse.Controllers
{
#pragma warning disable CS1591
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly PaymentDbContext DbContext;

        public TransactionController(PaymentDbContext dbContext)
        {
            DbContext = dbContext;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Places a new payment
        /// </summary>
        /// <param name="request">Payment request</param>
        /// <returns>A payment response</returns>
        [HttpPost("Payment")]
        public async Task<IActionResult> PostPayment([FromBody]PostPaymentRequest request)
        {
            var creditCards = await DbContext.GetCreditCardByCardHolderName(request.CardHolderName).ToListAsync();

            var creditCard = default(CreditCard);

            var last4Digits = request.CardNumber.Substring(request.CardNumber.Length - 4);

            if (creditCards.Count > 1)
            {
                creditCard = creditCards.FirstOrDefault(item => item.CardNumber == request.CardNumber);

                if (creditCard == null)
                    return BadRequest(string.Format("There isn't record for credit card with last 4 digits: {0}.", last4Digits));
            }
            else if (creditCards.Count == 1)
            {
                creditCard = creditCards.First();
            }
            else
            {
                return BadRequest(string.Format("There isn't record for credit card with last 4 digits: {0}.", request.CardNumber));
            }

            /* Check credit card information */

            if (!creditCard.IsValid(request))
                return BadRequest(string.Format("There is invalid data for credit card in this transaction."));

            /* Check if customer has available credit (limit) */

            if (!creditCard.HasFounds(request))
                return BadRequest(string.Format("There isn't founds to approve this transaction."));

            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var paymentTxn = new PaymentTransaction
                    {
                        PaymentTransactionID = Guid.NewGuid(),
                        CreditCardID = creditCard.CreditCardID,
                        ConfirmationID = Guid.NewGuid(),
                        Amount = request.Amount,
                        PaymentDateTime = DateTime.Now
                    };

                    DbContext.PaymentTransactions.Add(paymentTxn);

                    creditCard.AvailableFounds -= request.Amount;

                    await DbContext.SaveChangesAsync();

                    var response = new PaymentResponse
                    {
                        ConfirmationID = paymentTxn.ConfirmationID,
                        PaymentDateTime = paymentTxn.PaymentDateTime,
                        Last4Digits = creditCard.Last4Digits
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    txn.Rollback();

                    return new ObjectResult(ex.Message)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
            }
        }
    }
}
