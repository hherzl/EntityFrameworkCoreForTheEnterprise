namespace Store.Core.EntityLayer.Dbo
{
    public class ChangeLogExclusion : IEntity
    {
        public ChangeLogExclusion()
        {
        }

        public int? ChangeLogExclusionID { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }
    }
}
