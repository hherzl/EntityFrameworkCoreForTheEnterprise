using System.Text;
using System.Text.Json;

namespace RothschildHouse.Library.Common.Queue.Messages
{
    public record PublishTransactionMessage
    {
        public long? Id { get; set; }
        public Guid? Guid { get; set; }
        public string ClientApplication { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

        public byte[] ToBytes()
            => Encoding.UTF8.GetBytes(ToJson());

        public static PublishTransactionMessage DeserializeFrom(byte[] bytes)
                => JsonSerializer.Deserialize<PublishTransactionMessage>(Encoding.UTF8.GetString(bytes));
    }
}
