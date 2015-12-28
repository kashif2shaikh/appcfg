using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AppConfig.Core.Models;


namespace AppConfig.Core.Ef.Mapping {

    public class AuditMap : EntityTypeConfiguration<Audit> {

        public AuditMap() {
            this.ToTable("Audits");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.Version)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(x => x.IpAddress)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(x => x.AppName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(x => x.Environment)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(x => x.DateCreated);

            this.Property(x => x.ConfigSetId);
            this.HasOptional(x => x.ConfigSet)
                .WithMany(x => x.Audits)
                .HasForeignKey(x => x.ConfigSetId);
        }
    }
}
