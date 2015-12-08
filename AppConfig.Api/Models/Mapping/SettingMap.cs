using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;


namespace AppConfig.Api.Models.Mapping {

    public class SettingMap : EntityTypeConfiguration<Setting> {

        public SettingMap() {
            this.ToTable("Settings");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Key)
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UK_Key", 0) { IsUnique = true })
                )
                .IsRequired();
            this.Property(x => x.Value)
                .HasMaxLength(2000)
                .IsRequired();

            this.Property(x => x.ConfigSetId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UK_Key", 1) { IsUnique = true })
                );
            this.HasRequired(x => x.ConfigSet)
                .WithMany(x => x.Settings)
                .HasForeignKey(x => x.ConfigSetId);
        }
    }
}
