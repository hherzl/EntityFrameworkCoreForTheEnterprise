namespace OnlineStore.Core.Domain.Dbo
{
    public class ChangeLogExclusion : IEntity
    {
        public ChangeLogExclusion()
        {
        }

        public int? ID { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }
    }
}
