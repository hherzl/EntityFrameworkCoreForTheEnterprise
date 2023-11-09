using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;
using RothschildHouse.Application.Features.Cards.Queries;

namespace RothschildHouse.Application.Features.Transactions.Queries;

public record GetTransactionsViewBagRespose : Response
{
    public List<ListItem<short?>> TransactionStatuses { get; set; }
    public List<ListItem<Guid?>> ClientApplications { get; set; }
}

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
        var transactionStatus = await _dbContext.VTransactionStatus.ToListAsync(cancellationToken);

        var clientApplications = await _dbContext.ClientApplication.AsNoTracking().ToListAsync(cancellationToken);

        return new GetTransactionsViewBagRespose
        {
            TransactionStatuses = transactionStatus.Select(item => new ListItem<short?>((short)item.Id, item.Name)).ToList(),
            ClientApplications = clientApplications.Select(item => new ListItem<Guid?>(item.Id, item.Name)).ToList()
        };
    }
}
