using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace AppConfig.Api.Models.Mapping {

    public class ConfigSetMap : EntityTypeConfiguration<ConfigSet> {

        public ConfigSetMap() {
            this.ToTable("ConfigurationSets");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.EnvId);
            this.Property(x => x.AppId);

            this.HasOptional(x => x.Env)
                .WithMany(x => x.ConfigSets)
                .HasForeignKey(x => x.EnvId);

            this.HasRequired(x => x.App)
                .WithMany(x => x.ConfigSets)
                .HasForeignKey(x => x.AppId);

            this.HasMany(x => x.Settings)
                .WithRequired(x => x.ConfigSet)
                .HasForeignKey(x => x.ConfigSetId);

            this.HasMany(x => x.Audits)
                .WithOptional(x => x.ConfigSet)
                .HasForeignKey(x => x.ConfigSetId);
        }
    }
}
