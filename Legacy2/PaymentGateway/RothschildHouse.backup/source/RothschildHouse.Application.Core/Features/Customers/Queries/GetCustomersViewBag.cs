using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core.Features.Customers.Queries
{
    public class GetCustomersViewBagQuery : IRequest<GetCustomersViewBagRespose> { }

    public class GetCustomersViewBagQueryHandler : IRequestHandler<GetCustomersViewBagQuery, GetCustomersViewBagRespose>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetCustomersViewBagQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCustomersViewBagRespose> Handle(GetCustomersViewBagQuery request, CancellationToken cancellationToken)
        {
            var countries = await _dbContext.Country.AsNoTracking().ToListAsync(cancellationToken);

            var response = new GetCustomersViewBagRespose
            {
                Countries = countries.Select(item => new ListItem<short?>(item.Id, item.Name)).ToList()
            };

            return response;
        }
    }
}
