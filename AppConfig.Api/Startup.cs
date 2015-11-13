using System;
using System.Web.Http;
using Owin;


namespace AppConfig.Api {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            var cfg = new HttpConfiguration();
            cfg.MapHttpAttributeRoutes();
            cfg.EnsureInitialized();
            app.UseWebApi(cfg);
        }
    }
}
