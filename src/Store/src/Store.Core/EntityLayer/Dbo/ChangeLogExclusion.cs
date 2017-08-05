using System;

namespace Store.Core.EntityLayer.Dbo
{
    public class ChangeLogExclusion : IEntity
    {
        public ChangeLogExclusion()
        {
        }

        public String ChangeLogExclusionID { get; set; }

        public String EntityName { get; set; }

        public String PropertyName { get; set; }
    }
}
