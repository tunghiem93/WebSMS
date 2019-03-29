using CMS_Entity.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS_Entity.Mapping
{
    public class CMS_SimOperatorMap : EntityTypeConfiguration<CMS_SimOperator>
    {
        public CMS_SimOperatorMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.HeaderPhone).HasMaxLength(4).HasColumnType("varchar").IsRequired();
            this.Property(x => x.OperaterName).HasMaxLength(100).HasColumnType("varchar").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
        }
    }
}
