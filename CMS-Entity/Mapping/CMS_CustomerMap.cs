﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using CMS_Entity.Entity;

namespace CMS_Entity.Mapping
{
    public class CMS_CustomerMap : EntityTypeConfiguration<CMS_Customers>
    {
        public CMS_CustomerMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasMaxLength(60).HasColumnType("varchar").IsRequired();
            this.Property(x => x.Password2).HasMaxLength(250).HasColumnType("varchar").IsRequired();
            this.Property(x => x.CreatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.FirstName).HasMaxLength(50).HasColumnType("nvarchar").IsRequired();
            this.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            this.Property(x => x.Password).HasMaxLength(250).HasColumnType("varchar").IsRequired();
            this.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(15).IsOptional();
            this.Property(x => x.UpdatedBy).HasColumnType("varchar").HasMaxLength(60).IsOptional();
            this.Property(x => x.Status).HasColumnType("int").IsRequired();
            this.Property(x => x.TotalCredit).HasColumnType("decimal").IsOptional();
            this.Property(x => x.CreditNumber).HasMaxLength(60).HasColumnType("varchar").IsOptional();
            this.Property(x => x.APIKey).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            this.Property(x => x.APIPass).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            this.Property(x => x.IsVerifiedEmail).IsOptional();
            this.Property(x => x.IsVerifiedPhone).IsOptional();
            this.Property(x => x.SMSBalances).IsOptional();
            this.Property(x => x.MemberID).HasColumnType("varchar").HasMaxLength(100).IsOptional();
        }
    }
}
