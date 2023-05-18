using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class EnumDescription : Entity
    {
        public short? Id { get; set; }
        public string Type { get; set; }
        public long? Value { get; set; }
        public string Description { get; set; }
    }
}
