using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using AppConfig.Core.Models;


namespace AppConfig.Core.Ef.Mapping {

    public class EnvMap : EntityTypeConfiguration<Env> {

        public EnvMap() {
            this.ToTable("Environments");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Description).HasMaxLength(100);
            this.Property(x => x.AccessKey)
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UK_AccessKey") { IsUnique = true })
                );

            this.HasMany(x => x.ConfigSets)
                .WithOptional(x => x.Env)
                .HasForeignKey(x => x.EnvId);
        }
    }
}
