﻿using MediatR;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Features.Currencies.Queries
{
    public record CurrencyDetailsModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
    }

    public class GetCurrencyQuery : IRequest<ISingleResponse<CurrencyDetailsModel>>
    {
        public GetCurrencyQuery(short? id)
        {
            Id = id;
        }

        public short? Id { get; set; }
    }

    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, ISingleResponse<CurrencyDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCurrencyQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ISingleResponse<CurrencyDetailsModel>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCurrencyAsync(request.Id);

            if (entity == null)
                return null;

            return new SingleResponse<CurrencyDetailsModel>(new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                Rate = entity.Rate
            });
        }
    }
}
