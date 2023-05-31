using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Enums;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.Queries
{
    public record GetPaymentTransactionsViewBagRespose : Response
    {
        public List<ListItem<short>> PaymentTransactionStatuses { get; set; }
        public List<ListItem<Guid?>> ClientApplications { get; set; }
    }

    public class GetPaymentTransactionsViewBagQuery : IRequest<GetPaymentTransactionsViewBagRespose> { }

    public class GetPaymentTransactionsViewBagQueryHandler : IRequestHandler<GetPaymentTransactionsViewBagQuery, GetPaymentTransactionsViewBagRespose>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetPaymentTransactionsViewBagQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPaymentTransactionsViewBagRespose> Handle(GetPaymentTransactionsViewBagQuery request, CancellationToken cancellationToken)
        {
            var enumOptions = Enum.GetValues(typeof(PaymentTransactionStatus)).Cast<PaymentTransactionStatus>();

            var clientApplications = await _dbContext.ClientApplication.AsNoTracking().ToListAsync(cancellationToken);

            var response = new GetPaymentTransactionsViewBagRespose
            {
                PaymentTransactionStatuses = enumOptions.Select(item => new ListItem<short>((short)item, item.ToString())).ToList(),
                ClientApplications = clientApplications.Select(item => new ListItem<Guid?>(item.Id, item.Name)).ToList()
            };

            return response;
        }
    }
}
