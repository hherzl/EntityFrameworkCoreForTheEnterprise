using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;
using RothschildHouse.Application.Models;

namespace RothschildHouse.Application.Features.Transactions.Queries;

public class GetTransactionsQuery : IRequest<PagedResponse<TransactionItemModel>>
{
    public GetTransactionsQuery()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public short? TransactionStatusId { get; set; }
    public Guid? ClientApplicationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool IsEmpty
        => !TransactionStatusId.HasValue && !ClientApplicationId.HasValue && !StartDate.HasValue && !EndDate.HasValue;
}

public class GetPaymentTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedResponse<TransactionItemModel>>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetPaymentTransactionsQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<TransactionItemModel>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext
            .GetTransactions(transactionStatusId: request.TransactionStatusId, clientApplicationId: request.ClientApplicationId, startDate: request.StartDate, endDate: request.EndDate)
            .OrderByDescending(item => item.CreationDateTime)
            ;

        var list = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(cancellationToken)
            ;
        
        list.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

        return new PagedResponse<TransactionItemModel>(list, request.PageSize, request.PageNumber, await query.CountAsync(cancellationToken));
    }
}
