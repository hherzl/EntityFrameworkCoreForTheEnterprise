namespace RothschildHouse.API.PaymentGateway.Domain.Common
{
#pragma warning disable CS1591
    public abstract class AuditableEntity : Entity
    {
        public string CreationUser { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public byte[] Version { get; set; }
    }
}
