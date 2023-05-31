using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Application.Core.Features.Countries.Queries
{
    public record CountryItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
    }

    public class GetCountriesQuery : IRequest<ListResponse<CountryItemModel>>
    {
        public GetCountriesQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, ListResponse<CountryItemModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCountriesQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListResponse<CountryItemModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Country.AsNoTracking().Paging(request.PageSize, request.PageNumber);

            var model = await query.ToListAsync(cancellationToken);

            return new ListResponse<CountryItemModel>(model.Select(item => new CountryItemModel
            {
                Id = item.Id,
                Name = item.Name,
                TwoLetterIsoCode = item.TwoLetterIsoCode
            }));
        }
    }
}
