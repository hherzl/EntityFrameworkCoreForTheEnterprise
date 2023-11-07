using RothschildHouse.Domain.Common;

namespace RothschildHouse.Domain.Entities;

public partial class EnumDescription : Entity
{
    public short? Id { get; set; }
    public string Type { get; set; }
    public long? Value { get; set; }
    public string Description { get; set; }
}
