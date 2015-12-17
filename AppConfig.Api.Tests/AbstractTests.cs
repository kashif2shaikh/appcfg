using System;
using System.Threading.Tasks;
using AppConfig.Client;
using AppConfig.Client.ViewModels;
using Microsoft.Owin.Hosting;


namespace AppConfig.Api.Tests {

    public abstract class AbstractTests {
        protected virtual string Url { get; set; } = "http://localhost:6789";


        protected virtual async Task<AppConfiguration> GetConfig(string appName, string appVersion, string env) {
            var ver = Version.Parse(appVersion);

            using (WebApp.Start<Startup>(this.Url)) {
                var client = new AppConfigClient(this.Url);
                return await client.GetConfiguration(appName, ver, env);
            }
        }
    }
}
