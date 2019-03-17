using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_CustomerActiveCodeMap : EntityTypeConfiguration<CMS_CustomerActiveCode>
    {
        public CMS_CustomerActiveCodeMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.CustomerId).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.Code).HasMaxLength(11).HasColumnType("varchar").IsRequired();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.IsActive).IsRequired();
            this.HasRequired(x => x.Customer).WithMany(x => x.CustomerActiveCode).HasForeignKey(x => x.CustomerId);
        }
    }
}
