using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class EnumDescription : AuditableEntity
    {
        public EnumDescription()
        {
        }

        public short? Id { get; set; }
        public string Type { get; set; }
        public long? Value { get; set; }
        public string Description { get; set; }
    }
}
