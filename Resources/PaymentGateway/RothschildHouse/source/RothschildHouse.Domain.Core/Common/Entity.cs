using RothschildHouse.Domain.Core.Common.Contracts;

namespace RothschildHouse.Domain.Core.Common
{
    public class Entity : IEntity
    {
        public bool? Active { get; set; }
    }
}
