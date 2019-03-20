using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_DepositPackageMap : EntityTypeConfiguration<CMS_DepositPackage>
    {
        public CMS_DepositPackageMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.PackageName).HasMaxLength(250).HasColumnType("nvarchar").IsRequired();
            this.Property(x => x.PackageSMS).HasColumnType("decimal").IsRequired();
            //this.Property(x => x.PackagePrice).HasColumnType("decimal").IsRequired();
            this.Property(x => x.Discount).HasColumnType("decimal").IsRequired();
            this.Property(x => x.SMSPrice).HasColumnType("decimal").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
        }
    }
}
