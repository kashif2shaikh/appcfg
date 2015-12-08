using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;


namespace AppConfig.Api.Models.Mapping {

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
                .WithRequired(x => x.Env)
                .HasForeignKey(x => x.EnvId);
        }
    }
}
