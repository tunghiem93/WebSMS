using CMS_Entity.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS_Entity.Mapping
{
    public class CMS_GSMMap : EntityTypeConfiguration<CMS_GSM>
    {
        public CMS_GSMMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.GSMName).HasMaxLength(250).HasColumnType("nvarchar").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
        }
    }
}
