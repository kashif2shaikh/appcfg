using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Acr.Settings;
using AppConfig.Model;


namespace AppConfig.Api.Controllers {

    public class ConfigController : ApiController {
        readonly ISettings settings;


        public ConfigController() {
            this.settings = Settings.Local;
        }


        [HttpGet]
        [Route("~/{appName}/{appVersion}")]
        public AppConfiguration Get(string appName, string appVersion) {
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


        Dictionary<string, string> GetCustomParams(string rootKey) {
            // TODO: ignore url
            var dict = new Dictionary<string, string>();
            var keys = this.settings
                .List
                .Keys
                .Where(x => x.StartsWith(rootKey, true, CultureInfo.DefaultThreadCurrentCulture));

            foreach (var key in keys) {
                var newKey = key.Replace(rootKey + "/", String.Empty);
                var value = this.settings.Get<string>(key);
                dict.Add(newKey, value);
            }
            return dict;
        }
    }
}
