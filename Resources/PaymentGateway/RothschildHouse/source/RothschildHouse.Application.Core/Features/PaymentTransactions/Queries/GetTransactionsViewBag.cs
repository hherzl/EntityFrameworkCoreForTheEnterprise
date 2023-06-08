using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Transactions.Queries
{
    public class GetTransactionsViewBagQuery : IRequest<GetTransactionsViewBagRespose> { }

    public class GetTransactionsViewBagQueryHandler : IRequestHandler<GetTransactionsViewBagQuery, GetTransactionsViewBagRespose>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetTransactionsViewBagQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetTransactionsViewBagRespose> Handle(GetTransactionsViewBagQuery request, CancellationToken cancellationToken)
        {
            var enumOptions = Enum.GetValues(typeof(TransactionStatus)).Cast<TransactionStatus>();

            var clientApplications = await _dbContext.ClientApplication.AsNoTracking().ToListAsync(cancellationToken);

            var response = new GetTransactionsViewBagRespose
            {
                TransactionStatuses = enumOptions.Select(item => new ListItem<short?>((short)item, item.ToString())).ToList(),
                ClientApplications = clientApplications.Select(item => new ListItem<Guid?>(item.Id, item.Name)).ToList()
            };

            return response;
        }
    }
}
