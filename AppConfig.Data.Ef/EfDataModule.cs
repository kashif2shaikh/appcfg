using System;
using AppConfig.Api;
using Autofac;


namespace AppConfig.Data.Ef {

    public class EfDataModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
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
