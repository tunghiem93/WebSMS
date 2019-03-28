using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_PaymentMethodMap : EntityTypeConfiguration<CMS_PaymentMethod>
    {
        public CMS_PaymentMethodMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.PaymentName).HasMaxLength(250).HasColumnType("nvarchar").IsRequired();
            this.Property(x => x.PaymentType).HasColumnType("int").IsRequired();
            this.Property(x => x.WalletMoney).HasMaxLength(100).HasColumnType("varchar").IsOptional();
            this.Property(x => x.TagContent).HasMaxLength(250).HasColumnType("nvarchar").IsUnicode(true).IsOptional();
            this.Property(x => x.ScaleNumber).HasColumnType("int").IsOptional();
            this.Property(x => x.ReferenceExchange).HasColumnType("int").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.URLApi).HasColumnType("nvarchar").HasMaxLength(500).IsOptional();
        }
    }
}
