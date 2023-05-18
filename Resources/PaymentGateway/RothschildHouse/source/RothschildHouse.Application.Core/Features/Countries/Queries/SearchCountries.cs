using MediatR;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Countries.Queries
{
    public record CountryItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
    }

    public class SearchCountriesQuery : IRequest<IListResponse<CountryItemModel>>
    {
        public SearchCountriesQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class SearchCountriesQueryHandler : IRequestHandler<SearchCountriesQuery, IListResponse<CountryItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public SearchCountriesQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IListResponse<CountryItemModel>> Handle(SearchCountriesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new ListResponse<CountryItemModel>
            {
                Model = new List<CountryItemModel>
                {
                    new CountryItemModel
                    {
                        Id = 503,
                        Name = "El Salvador",
                        TwoLetterIsoCode = "SV"
                    }
                }
            });
        }
    }
}
