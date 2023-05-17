using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.ClientApplications.Queries
{
#pragma warning disable CS1591
    public class GetClientApplicationQuery : IRequest<SingleResponse<ClientApplicationDetailsModel>>
    {
        public GetClientApplicationQuery()
        {
        }

        public GetClientApplicationQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetClientApplicationQueryHandler : IRequestHandler<GetClientApplicationQuery, SingleResponse<ClientApplicationDetailsModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public GetClientApplicationQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<ClientApplicationDetailsModel>> Handle(GetClientApplicationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetClientApplicationAsync(request.Id);

            if (entity == null)
                return SingleResponse<ClientApplicationDetailsModel>.Empty;

            var paymentTransactions = await _dbContext
                .GetPaymentTransactions(clientApplicationId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            return new SingleResponse<ClientApplicationDetailsModel>
            {
                Model = new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Url = entity.Url,
                    PaymentTransactions = paymentTransactions
                }
            };
        }
    }
}
