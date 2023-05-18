using RothschildHouse.Domain.Core.Common.Contracts;

namespace RothschildHouse.Domain.Core.Common
{
    public class AuditableEntity : Entity, IAuditableEntity
    {
        public string CreationUser { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public byte[] Version { get; set; }
    }
}
