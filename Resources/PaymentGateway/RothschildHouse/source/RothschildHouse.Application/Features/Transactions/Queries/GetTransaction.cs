using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Transactions.Queries;

public record TransactionDetailsModel
{
    public TransactionDetailsModel()
    {
        Logs = new();
    }

    public long? Id { get; set; }
    public short? TransactionStatusId { get; set; }
    public string TransactionStatus { get; set; }
    public Guid? ClientApplicationId { get; set; }
    public string ClientApplication { get; set; }
    public Guid? CustomerId { get; set; }
    public string Customer { get; set; }
    public string CardType { get; set; }
    public string IssuingNetwork { get; set; }
    public string CardholderName { get; set; }
    public decimal? OrderTotal { get; set; }
    public string Currency { get; set; }
    public decimal? CurrencyRate { get; set; }
    public DateTime? CreationDateTime { get; set; }

    public List<TransactionLogDetailsModel> Logs { get; set; }
}

public record TransactionLogDetailsModel
{
    public long? Id { get; set; }
    public short? TransactionStatusId { get; set; }
    public string TransactionStatus { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
    public string Notes { get; set; }
    public DateTime? CreationDateTime { get; set; }
}

public class GetTransactionQuery : IRequest<SingleResponse<TransactionDetailsModel>>
{
    public GetTransactionQuery(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
}

public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, SingleResponse<TransactionDetailsModel>>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetTransactionQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SingleResponse<TransactionDetailsModel>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var txn = await _dbContext.GetTransactionAsync(request.Id, tracking: false, cancellationToken: cancellationToken);

        if (txn == null)
            return null;

        var txnStatuses = await _dbContext.VTransactionStatus.ToListAsync(cancellationToken);
        var cardTypes = await _dbContext.VCardType.ToListAsync(cancellationToken);
        var customer = txn.CustomerFk.PersonId.HasValue ? txn.CustomerFk.PersonFk.FullName : txn.CustomerFk.CompanyFk.Name;

        var detailsModel = new TransactionDetailsModel
        {
            Id = txn.Id,
            TransactionStatusId = txn.TransactionStatusId,
            TransactionStatus = txnStatuses.FirstOrDefault(item => item.Id == txn.TransactionStatusId)?.Name,
            ClientApplicationId = txn.ClientApplicationId,
            ClientApplication = txn.ClientApplicationFk.Name,
            CustomerId = txn.CustomerId,
            Customer = customer,
            CardType = cardTypes.FirstOrDefault(item => item.Id == txn.CardFk.CardTypeId)?.Name,
            IssuingNetwork = txn.CardFk.IssuingNetwork,
            CardholderName = txn.CardFk.CardholderName,
            OrderTotal = txn.Amount,
            Currency = txn.CurrencyFk.Name,
            CurrencyRate = txn.CurrencyRate,
            CreationDateTime = txn.CreationDateTime
        };

        if (txn.TransactionLogList.Count > 0)
        {
            foreach (var logItem in txn.TransactionLogList)
            {
                var logDetails = new TransactionLogDetailsModel
                {
                    Id = logItem.Id,
                    TransactionStatusId = logItem.TransactionStatusId,
                    TransactionStatus = txnStatuses.FirstOrDefault(item => item.Id == logItem.TransactionStatusId)?.Name,
                    LogType = logItem.LogType,
                    ContentType = logItem.ContentType,
                    Content = logItem.Content, //.Replace("\\r\\n", Environment.NewLine).Replace("\\u0022", "\""),
                    Notes = logItem.Notes,
                    CreationDateTime = logItem.CreationDateTime
                };

                detailsModel.Logs.Add(logDetails);
            }
        }

        return new SingleResponse<TransactionDetailsModel>(detailsModel);
    }
}
