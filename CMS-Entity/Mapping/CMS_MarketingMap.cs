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
            this.Property(x => x.CustomerId).HasColumnType("varchar").HasMaxLength(60);
            this.Property(x => x.CustomerName).HasColumnType("nvarchar").HasMaxLength(250);
            this.Property(x => x.SMSContent).HasMaxLength(1000).HasColumnType("nvarchar").HasMaxLength(1000).IsOptional();
            this.Property(x => x.SendTo).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.SendFrom).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.SMSPrice).HasColumnType("decimal").IsOptional();
            this.Property(x => x.OperatorName).HasColumnType("nvarchar").HasMaxLength(100);
            this.Property(x => x.Status).HasColumnType("int").IsOptional();
            this.Property(x => x.SMSType).HasColumnType("int").IsOptional();
            this.Property(x => x.RunTime).HasColumnType("decimal").IsOptional();
            this.Property(x => x.TimeInput).HasColumnType("datetime").IsOptional();
            this.Property(x => x.UpdatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.CreatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
        }
    }
}
