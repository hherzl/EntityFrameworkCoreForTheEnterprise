using System.Text.Json;
using System.Text;

namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record SearchClientApplicationsRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);
    }
}
