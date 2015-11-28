using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Acr.Settings;
using AppConfig.Model;


namespace AppConfig.Api.Controllers {

    public class ConfigController : ApiController {


        public ConfigController() {
        }


        // TODO: version range
        // TODO: environment - PRODWEB5 or Android or iOS, etc
        // TODO: environment is not required
        [HttpGet]
        [Route("~/{appName}/{appVersion}/{environment}")]
        public AppConfiguration Get(string appName, string appVersion, string environment = null) {
            var ver = Version.Parse(appVersion); // TODO: version range
            var cfg = new AppConfiguration {
                ApplicationName = appName,
                Version = ver
            };

            var isAppKnown = this.settings.List.Keys.Any(x => x.StartsWith(appName, true, CultureInfo.DefaultThreadCurrentCulture));
            if (!isAppKnown) {
                cfg.Status = ConfigStatus.UnknownApp;
                return cfg;
            }

            var rootKey = $"{appName}/{appVersion}";
            var isVersionKnown = this.settings.List.Keys.Any(x => x.StartsWith(rootKey, true, CultureInfo.DefaultThreadCurrentCulture));
            if (!isVersionKnown) {
                cfg.Status = ConfigStatus.UnknownVersion;
                return cfg;
            }

            cfg.BaseApiUrl = this.settings.Get<string>($"{rootKey}/url");
            cfg.CustomParameters = this.GetCustomParams(rootKey);
            cfg.Status = ConfigStatus.Success;

            return cfg;
        }
    }
}
