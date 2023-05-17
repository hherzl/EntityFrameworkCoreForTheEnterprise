using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Customers.Queries
{
#pragma warning disable CS1591
    public class GetCustomerQuery : IRequest<SingleResponse<CustomerDetailsModel>>
    {
        public Guid? Id { get; set; }
        public Guid? AlienGuid { get; set; }
    }

    public class GetPaymentTransactionQueryHandler : IRequestHandler<GetCustomerQuery, SingleResponse<CustomerDetailsModel>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CustomerDetailsModel>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Customer entity = null;

            if (request.Id.HasValue)
                entity = await _dbContext.GetCustomerAsync(request.Id, false, true, cancellationToken);
            else if (request.AlienGuid.HasValue)
                entity = await _dbContext.GetCustomerByAlienGuidAsync(request.AlienGuid, false, true, cancellationToken);

            if (entity == null)
                return SingleResponse<CustomerDetailsModel>.Empty;

            var paymentTransactions = await _dbContext
                .GetPaymentTransactions(customerId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            return new SingleResponse<CustomerDetailsModel>
            {
                Model = new()
                {
                    Id = entity.Id,
                    PersonId = entity.PersonId,
                    CompanyId = entity.CompanyId,
                    CountryId = entity.CountryId,
                    AddressLine1 = entity.AddressLine1,
                    AddressLine2 = entity.AddressLine2,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    AlienGuid = entity.AlienGuid,
                    PaymentTransactions = paymentTransactions
                }
            };
        }
    }
}
