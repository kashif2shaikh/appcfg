using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using AppConfig.Core.Models;


namespace AppConfig.Core.Ef.Mapping {

    public class AppMap : EntityTypeConfiguration<App> {

        public AppMap() {
            this.ToTable("Applications");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(x => x.ClientSecret)
                .HasMaxLength(255)
                .IsRequired();

            this.Property(x => x.IsActive);

            this.Property(x => x.AccessKey)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UK_AccessKey") { IsUnique = true })
                )
                .HasMaxLength(50);

            this.HasMany(x => x.ConfigSets)
                .WithRequired(x => x.App)
                .HasForeignKey(x => x.AppId);
        }
    }
}
