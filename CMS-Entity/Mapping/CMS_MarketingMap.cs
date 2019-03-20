using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_MarketingMap : EntityTypeConfiguration<CMS_Marketing>
    {
        public CMS_MarketingMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnType("varchar").HasMaxLength(60);
            this.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(60);
            this.Property(x => x.Message).HasColumnType("nvarchar").HasMaxLength(1000);
            this.Property(x => x.NetworkType).HasColumnType("int").IsOptional();
            this.Property(x => x.SMSType).HasColumnType("int").IsOptional();
            this.Property(x => x.RunTime).HasColumnType("decimal").IsOptional();
            this.Property(x => x.Cost).HasColumnType("decimal").IsOptional();
            this.Property(x => x.OperatorName).HasColumnType("nvarchar").HasMaxLength(100);
            this.Property(x => x.Status).HasColumnType("int").IsOptional();
            this.Property(x => x.UpdatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.CreatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.HasRequired(x => x.Customer).WithMany(x => x.Marketing).HasForeignKey(x => x.CustomerId);
        }
    }
}
