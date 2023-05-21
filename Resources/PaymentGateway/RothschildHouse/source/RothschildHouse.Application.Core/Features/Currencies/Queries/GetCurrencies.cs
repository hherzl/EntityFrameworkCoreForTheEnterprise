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

    public class GetCurrenciesQuery : IRequest<IListResponse<CurrencyItemModel>>
    {
        public GetCurrenciesQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IListResponse<CurrencyItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCurrenciesQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IListResponse<CurrencyItemModel>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
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
}
