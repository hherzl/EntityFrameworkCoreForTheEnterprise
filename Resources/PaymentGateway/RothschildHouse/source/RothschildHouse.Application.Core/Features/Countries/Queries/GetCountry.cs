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
            return await Task.FromResult(new SingleResponse<CountryDetailsModel>
            {
                Model = new CountryDetailsModel
                {
                    Id = 503,
                    Name = "El Salvador",
                    TwoLetterIsoCode = "SV",
                    ThreeLetterIsoCode = "SLV"
                }
            });
        }
    }
}
