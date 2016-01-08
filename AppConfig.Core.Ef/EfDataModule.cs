using System;
using Autofac;
using HibernatingRhinos.Profiler.Appender.EntityFramework;


namespace AppConfig.Core.Ef {

    public class EfDataModule : Module {
        public bool IsDebugEnabled { get; set; }


        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            if (this.IsDebugEnabled)
                EntityFrameworkProfiler.Initialize();

            builder
                .Register(x => new CfgDbContext())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ConfigManagerImpl>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
