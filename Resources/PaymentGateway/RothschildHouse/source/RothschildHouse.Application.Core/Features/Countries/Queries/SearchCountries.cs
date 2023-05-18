using MediatR;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Client.DataContracts;
using RothschildHouse.Library.Client.DataContracts.Common;
using RothschildHouse.Library.Client.DataContracts.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Countries.Queries
{
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
                        TwoLetterIsoCode = "SV",
                        ThreeLetterIsoCode = "SLV"
                    }
                }
            });
        }
    }
}
