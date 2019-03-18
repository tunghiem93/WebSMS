using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Mapping
{
    public class CMS_TimesMap : EntityTypeConfiguration<CMS_Times>
    {
        public CMS_TimesMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar");
            this.Property(x => x.WaitingTime).HasColumnType("int").IsRequired();
        }
    }
}
