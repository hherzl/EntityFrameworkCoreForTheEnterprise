using System;

namespace Store.Core.EntityLayer
{
    public class ChangeLog : IEntity
    {
        public ChangeLog()
        {
        }

        public Int32? ChangeLogID { get; set; }

        public String ClassName { get; set; }

        public String PropertyName { get; set; }

        public String Key { get; set; }

        public String OriginalValue { get; set; }

        public String CurrentValue { get; set; }

        public String UserName { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}
