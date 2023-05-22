using MediatR;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Countries.Queries
{
    public record CountryDetailsModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
    }

    public class GetCountryQuery : IRequest<ISingleResponse<CountryDetailsModel>>
    {
        public GetCountryQuery(short? id)
        {
            Id = id;
        }

        public short? Id { get; set; }
    }

    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, ISingleResponse<CountryDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCountryQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ISingleResponse<CountryDetailsModel>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCountryAsync(request.Id);

            if (entity == null)
                return null;

            return new SingleResponse<CountryDetailsModel>(new CountryDetailsModel
            {
                Id = entity.Id,
                Name = entity.Name,
                TwoLetterIsoCode = entity.TwoLetterIsoCode,
                ThreeLetterIsoCode = entity.ThreeLetterIsoCode
            });
        }
    }
}
