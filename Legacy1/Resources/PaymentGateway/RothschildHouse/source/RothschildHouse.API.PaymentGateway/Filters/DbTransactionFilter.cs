using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;

namespace RothschildHouse.API.PaymentGateway.Filters
{
#pragma warning disable CS1591
    public class DbTransactionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly RothschildHouseDbContext _dbContext;
        private IDbContextTransaction _requestDbContextTransaction;

        public DbTransactionFilter(ILogger<DbTransactionFilter> logger, RothschildHouseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("OnActionExecuting()");

            _logger.LogInformation("Begin transaction...");

            _requestDbContextTransaction = _dbContext.Database.BeginTransaction();

            _logger.LogInformation($"Transaction {_requestDbContextTransaction.TransactionId} has been started");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted()");

            if (context.Exception == null)
            {
                _logger.LogInformation("Commit transaction");
                _requestDbContextTransaction?.Commit();
            }
            else
            {
                _logger.LogInformation("Rolling back transaction");
                _requestDbContextTransaction?.Rollback();
            }
        }
    }
}
