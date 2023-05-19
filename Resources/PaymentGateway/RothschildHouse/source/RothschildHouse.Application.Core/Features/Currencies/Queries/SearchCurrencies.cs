using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Currencies.Queries
{
    public record CurrencyItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
    }

    public class SearchCurrenciesQuery : IRequest<IListResponse<CurrencyItemModel>>
    {
        public SearchCurrenciesQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class SearchCurrenciesQueryHandler : IRequestHandler<SearchCurrenciesQuery, IListResponse<CurrencyItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public SearchCurrenciesQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IListResponse<CurrencyItemModel>> Handle(SearchCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await _dbContext.Currency.AsNoTracking().ToListAsync(cancellationToken);

            return new ListResponse<CurrencyItemModel>(currencies.Select(item => new CurrencyItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Code
            }));
        }
    }
}
