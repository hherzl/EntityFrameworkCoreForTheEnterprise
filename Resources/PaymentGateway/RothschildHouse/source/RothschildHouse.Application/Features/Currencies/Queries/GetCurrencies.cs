﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Common;
using RothschildHouse.Application.Common.Contracts;

namespace RothschildHouse.Application.Features.Currencies.Queries;

public record CurrencyItemModel
{
    public short? Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public decimal? Rate { get; set; }
}

public class GetCurrenciesQuery : IRequest<ListResponse<CurrencyItemModel>>
{
    public GetCurrenciesQuery()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, ListResponse<CurrencyItemModel>>
{
    private readonly IRothschildHouseDbContext _dbContext;

    public GetCurrenciesQueryHandler(IRothschildHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListResponse<CurrencyItemModel>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Currency.AsNoTracking().Paging(request.PageSize, request.PageNumber);

        var model = await query.ToListAsync(cancellationToken);

        return new ListResponse<CurrencyItemModel>(model.Select(item => new CurrencyItemModel
        {
            Id = item.Id,
            Name = item.Name,
            Code = item.Code
        }));
    }
}
