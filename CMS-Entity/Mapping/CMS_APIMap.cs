using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_APIMap : EntityTypeConfiguration<CMS_API>
    {
        public CMS_APIMap()
        { 
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar");
            this.Property(x => x.LinkAPI).HasMaxLength(200).IsRequired().HasColumnType("nvarchar");
            this.Property(x => x.APIName).HasMaxLength(200).IsRequired().HasColumnType("nvarchar");
            this.Property(x => x.APIType).HasColumnType("int").IsOptional();
            this.Property(x => x.Description).IsOptional().HasColumnType("ntext");
            this.Property(x => x.UpdatedBy).HasMaxLength(60).IsOptional().HasColumnType("varchar");
            this.Property(x => x.CreatedBy).HasMaxLength(60).IsOptional().HasColumnType("varchar");
            this.Property(x => x.UpdatedDate).IsOptional();
        }
    }
}
