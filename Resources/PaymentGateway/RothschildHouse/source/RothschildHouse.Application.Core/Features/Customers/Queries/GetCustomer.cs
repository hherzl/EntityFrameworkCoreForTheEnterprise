using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Customers.Queries
{
    public class GetCustomerQuery : IRequest<SingleResponse<CustomerDetailsModel>>
    {
        public GetCustomerQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, SingleResponse<CustomerDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCustomerQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CustomerDetailsModel>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCustomerAsync(request.Id, cancellationToken, false);

            entity ??= await _dbContext.GetCustomerByAlienGuidAsync(request.Id, cancellationToken, false, true);

            if (entity == null)
                return null;

            var transactions = await _dbContext
                .GetTransactions(customerId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            transactions.ForEach(item => item.CardNumber = item.CardNumber?[^4..]);

            return new SingleResponse<CustomerDetailsModel>
            {
                Model = new()
                {
                    Id = entity.Id,
                    PersonId = entity.PersonId,
                    Person = entity.PersonFk?.FullName,
                    CompanyId = entity.CompanyId,
                    Company = entity.CompanyFk?.Name,
                    CountryId = entity.CountryId,
                    Country = entity.CountryFk.Name,
                    AddressLine1 = entity.AddressLine1,
                    AddressLine2 = entity.AddressLine2,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    AlienGuid = entity.AlienGuid,
                    Transactions = transactions
                }
            };
        }
    }
}
