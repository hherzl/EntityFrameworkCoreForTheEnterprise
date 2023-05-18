namespace RothschildHouse.Domain.Core.Common.Contracts
{
    public interface IAuditableEntity : IEntity
    {
        public string CreationUser { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public byte[] Version { get; set; }
    }
}
