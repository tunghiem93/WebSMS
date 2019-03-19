using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_SysConfigMap : EntityTypeConfiguration<CMS_SysConfigs>
    {
        public CMS_SysConfigMap()
        {

            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnType("varchar").HasMaxLength(60);
            this.Property(x => x.Value).HasColumnType("decimal").IsRequired();
            this.Property(x => x.ValueType).HasColumnType("int").IsRequired();
            this.Property(x => x.UpdatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.SiteContent).HasColumnType("ntext");
            this.Property(x => x.CreatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
        }
    }
}
