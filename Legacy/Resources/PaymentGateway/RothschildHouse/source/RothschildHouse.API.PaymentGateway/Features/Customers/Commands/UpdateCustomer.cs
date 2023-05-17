using MediatR;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Customers.Commands
{
#pragma warning disable CS1591
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public UpdateCustomerCommandHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.GetCustomerByAlienGuidAsync(request.UCommerceGuid, true, true, cancellationToken);

            if (customer == null)
                return null;

            customer.Phone = request.Phone;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response();
        }
    }
}
