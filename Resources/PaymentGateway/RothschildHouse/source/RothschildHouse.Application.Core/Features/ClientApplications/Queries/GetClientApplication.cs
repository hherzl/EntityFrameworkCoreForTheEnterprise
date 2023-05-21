﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Features.PaymentTransactions.Queries;

namespace RothschildHouse.Application.Core.Features.ClientApplications.Queries
{
    public record ClientApplicationDetailsModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }

    public class GetClientApplicationQuery : IRequest<ISingleResponse<ClientApplicationDetailsModel>>
    {
        public GetClientApplicationQuery(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }

    public class GetClientApplicationQueryHandler : IRequestHandler<GetClientApplicationQuery, ISingleResponse<ClientApplicationDetailsModel>>
    {
        private readonly IRothschildHouseDbContext _dbContext;

        public GetClientApplicationQueryHandler(IRothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ISingleResponse<ClientApplicationDetailsModel>> Handle(GetClientApplicationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.GetClientApplicationAsync(request.Id);

            if (entity == null)
                return null;

            var paymentTransactions = await _dbContext
                .GetPaymentTransactions(clientApplicationId: entity.Id)
                .OrderByDescending(item => item.CreationDateTime)
                .Paging(10, 1)
                .ToListAsync(cancellationToken)
                ;

            return new SingleResponse<ClientApplicationDetailsModel>(new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = entity.Url,
                PaymentTransactions = paymentTransactions
            });
        }
    }
}