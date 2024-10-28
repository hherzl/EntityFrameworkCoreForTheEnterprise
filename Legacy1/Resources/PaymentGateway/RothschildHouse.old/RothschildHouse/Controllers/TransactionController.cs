using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RothschildHouse.Domain;
using RothschildHouse.Requests;
using RothschildHouse.Responses;

namespace RothschildHouse.Controllers
{
#pragma warning disable CS1591
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        readonly ILogger<TransactionController> Logger;
        readonly PaymentDbContext DbContext;

        public TransactionController(ILogger<TransactionController> logger, PaymentDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Places a new payment
        /// </summary>
        /// <param name="request">Payment request</param>
        /// <returns>A payment response</returns>
        [HttpPost("Payment")]
        public async Task<IActionResult> PostPaymentAsync([FromBody]PostPaymentRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostPaymentAsync));

            var creditCards = await DbContext.GetCreditCardByCardHolderName(request.CardHolderName).ToListAsync();

            var creditCard = default(CreditCard);

            var last4Digits = request.CardNumber.Substring(request.CardNumber.Length - 4);

            if (creditCards.Count > 1)
                creditCard = creditCards.FirstOrDefault(item => item.CardNumber == request.CardNumber);
            else if (creditCards.Count == 1)
                creditCard = creditCards.First();

            if (creditCard == null)
                return BadRequest(string.Format("There is not record for credit card with last 4 digits: {0}.", last4Digits));

            /* Check credit card information */

            if (!creditCard.IsValid(request))
                return BadRequest(string.Format("Invalid information for card payment."));

            /* Check if customer has available credit (limit) */

            if (!creditCard.HasFounds(request))
                return BadRequest(string.Format("There are no founds to approve the payment."));

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

                    txn.Commit();

                    Logger?.LogInformation("The payment for card with last 4 digits: '{0}' was successfully. Confirmation #: {1}", last4Digits, paymentTxn.ConfirmationID);

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
                    Logger?.LogCritical("There was an error on '{0}': {1}", nameof(PostPaymentAsync), ex);

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
