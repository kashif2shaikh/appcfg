using System;
using System.Data.Entity;
using System.Reflection;
using AppConfig.Core.Models;


namespace AppConfig.Core.Ef {

    public class CfgDbContext : DbContext {

        public DbSet<App> Applications { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<ConfigSet> ConfigSets { get; set; }
        public DbSet<Env> Environments { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ComplexType<VersionComponent>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
