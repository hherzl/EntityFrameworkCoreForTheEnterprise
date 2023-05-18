using System.Text;
using System.Text.Json;

namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record SearchCountriesRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);
    }
}
