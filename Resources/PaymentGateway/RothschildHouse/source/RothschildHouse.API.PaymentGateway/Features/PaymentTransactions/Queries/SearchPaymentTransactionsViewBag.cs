using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Domain.Enums;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.PaymentTransactions.Queries
{
#pragma warning disable CS1591
    public class SearchPaymentTransactionsViewBagQuery : IRequest<SearchPaymentTransactionsViewBagRespose> { }

    public class SearchPaymentTransactionsQueryHandler : IRequestHandler<SearchPaymentTransactionsViewBagQuery, SearchPaymentTransactionsViewBagRespose>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public SearchPaymentTransactionsQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SearchPaymentTransactionsViewBagRespose> Handle(SearchPaymentTransactionsViewBagQuery request, CancellationToken cancellationToken)
        {
            var enumOptions = Enum.GetValues(typeof(PaymentTransactionStatus)).Cast<PaymentTransactionStatus>();

            var clientApplications = await _dbContext.ClientApplication.AsNoTracking().ToListAsync(cancellationToken);

            var response = new SearchPaymentTransactionsViewBagRespose
            {
                PaymentTransactionStatuses = enumOptions.Select(item => new ListItem<short>((short)item, item.ToString())).ToList(),
                ClientApplications = clientApplications.Select(item => new ListItem<Guid?>(item.Id, item.Name)).ToList()
            };

            return response;
        }
    }
}
