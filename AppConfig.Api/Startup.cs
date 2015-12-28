using System;
using System.Web.Http;
using AppConfig.Core.Ef;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;


namespace AppConfig.Api {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers();
            builder.RegisterModule(new EfDataModule());
            var container = builder.Build();

            var cfg = new HttpConfiguration {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };
            cfg.MapHttpAttributeRoutes();
            cfg.EnsureInitialized();
            app.UseWebApi(cfg);
        }
    }
}
