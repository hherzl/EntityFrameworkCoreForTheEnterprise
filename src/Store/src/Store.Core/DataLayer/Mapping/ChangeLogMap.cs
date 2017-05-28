using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Mapping
{
    [Export(typeof(IEntityMap))]
    public class ChangeLogMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ChangeLog>();

            entity.ToTable("ChangeLog", "dbo");

            entity.HasKey(p => p.ChangeLogID);

            entity.Property(p => p.ChangeLogID).UseSqlServerIdentityColumn();
        }
    }
}
