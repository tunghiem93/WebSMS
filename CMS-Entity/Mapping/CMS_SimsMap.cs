using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_SimsMap : EntityTypeConfiguration<CMS_Sims>
    {
        public CMS_SimsMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.SimName).HasMaxLength(250).HasColumnType("nvarchar").IsRequired();
            this.Property(x => x.SimNumber).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.OperatorName).HasColumnType("nvarchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.Status).HasColumnType("int").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
        }
    }
}
