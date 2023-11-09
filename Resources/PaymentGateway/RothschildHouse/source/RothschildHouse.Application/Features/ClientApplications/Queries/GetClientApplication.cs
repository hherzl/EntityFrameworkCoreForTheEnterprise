using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;
using RothschildHouse.Application.Models;

namespace RothschildHouse.Application.Features.ClientApplications.Queries;

public record ClientApplicationDetailsModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public List<TransactionItemModel> Transactions { get; set; }
}

public class GetClientApplicationQuery : IRequest<SingleResponse<ClientApplicationDetailsModel>>
{
    public GetClientApplicationQuery(Guid? id)
    {
        Id = id;
    }

    public Guid? Id { get; set; }
}

public class GetClientApplicationQueryHandler : IRequestHandler<GetClientApplicationQuery, SingleResponse<ClientApplicationDetailsModel>>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetClientApplicationQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SingleResponse<ClientApplicationDetailsModel>> Handle(GetClientApplicationQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetClientApplicationAsync(request.Id);

        if (entity == null)
            return null;

        var transactions = await _dbContext
            .GetTransactions(clientApplicationId: entity.Id)
            .OrderByDescending(item => item.CreationDateTime)
            .Paging(10, 1)
            .ToListAsync(cancellationToken);

        transactions.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

        return new SingleResponse<ClientApplicationDetailsModel>(new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Url = entity.Url,
            Transactions = transactions
        });
    }
}
