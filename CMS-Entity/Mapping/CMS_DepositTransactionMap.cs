using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_DepositTransactionMap : EntityTypeConfiguration<CMS_DepositTransactions>
    {
        public CMS_DepositTransactionMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.CustomerId).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.CustomerName).HasMaxLength(250).HasColumnType("nvarchar").IsOptional();
            this.Property(x => x.WalletMoney).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.PackageId).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.PackageName).HasMaxLength(250).HasColumnType("nvarchar").IsOptional();
            this.Property(x => x.PackageSMS).HasColumnType("decimal").HasPrecision(18,2).IsOptional();
            this.Property(x => x.SMSPrice).HasColumnType("decimal").HasPrecision(18, 2).IsOptional();
            this.Property(x => x.ExchangeRate).HasColumnType("decimal").HasPrecision(18, 2).IsOptional();
            this.Property(x => x.PackagePrice).HasColumnType("decimal").HasPrecision(18, 2).IsOptional();
            this.Property(x => x.PaymentMethodId).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.PaymentMethodName).HasMaxLength(250).HasColumnType("nvarchar").IsOptional();
            this.Property(x => x.PayCoin).HasColumnType("decimal").HasPrecision(18, 2).IsOptional();
            this.Property(x => x.Status).HasColumnType("int").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.DepositNo).HasColumnType("varchar").HasMaxLength(60).IsOptional();
        }
    }
}
