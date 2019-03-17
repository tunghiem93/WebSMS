using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_ConfigRatesMap : EntityTypeConfiguration<CMS_ConfigRates>
    {
        public CMS_ConfigRatesMap()
        {

            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnType("varchar").HasMaxLength(60);
            this.Property(x => x.Rate).HasColumnType("decimal").IsRequired();
            this.Property(x => x.RateType).HasColumnType("int").IsRequired();
            this.Property(x => x.UpdatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.CreatedBy).HasMaxLength(60).HasColumnType("varchar").IsOptional();
        }
    }
}
