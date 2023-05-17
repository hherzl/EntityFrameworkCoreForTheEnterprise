using System.Text;
using System.Text.Json;
using MediatR;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public class SearchPaymentTransactionsQuery : IRequest<PagedResponse<PaymentTransactionItemModel>>
    {
        public SearchPaymentTransactionsQuery()
        {
            PageSize = 25;
            PageNumber = 1;
        }

        public SearchPaymentTransactionsQuery(short? paymentTransactionStatusId, Guid? clientApplicationId, DateTime? startDate, DateTime? endDate)
        {
            PageSize = 25;
            PageNumber = 1;
            PaymentTransactionStatusId = paymentTransactionStatusId;
            ClientApplicationId = clientApplicationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public short? PaymentTransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);
    }
}
