using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Features.PaymentTransactions.Queries;

namespace RothschildHouse.Application.Core.Features.Customers.Queries
{
    public record CustomerDetailsModel
    {
        public Guid? Id { get; set; }
        public int? PersonId { get; set; }
        public string Person { get; set; }
        public int? CompanyId { get; set; }
        public string Company { get; set; }
        public short? CountryId { get; set; }
        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }

    public class GetCustomerQuery : IRequest<SingleResponse<CustomerDetailsModel>>
    {
        public GetCustomerQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetPaymentTransactionQueryHandler : IRequestHandler<GetCustomerQuery, SingleResponse<CustomerDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetPaymentTransactionQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SingleResponse<CustomerDetailsModel>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetCustomerAsync(request.Id, false, true, cancellationToken);

            if (entity == null)
                entity = await _dbContext.GetCustomerByAlienGuidAsync(request.Id, false, true, cancellationToken);

            if (entity == null)
                return null;

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
