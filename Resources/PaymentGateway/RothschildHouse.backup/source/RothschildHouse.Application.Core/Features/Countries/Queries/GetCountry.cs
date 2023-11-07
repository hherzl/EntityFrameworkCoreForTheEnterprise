using MediatR;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Countries.Queries
{
    public class GetCountryQuery : IRequest<SingleResponse<CountryDetailsModel>>
    {
        public GetCountryQuery(short? id)
        {
            Id = id;
        }

        public short? Id { get; set; }
    }

    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, SingleResponse<CountryDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCountryQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CountryDetailsModel>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
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
