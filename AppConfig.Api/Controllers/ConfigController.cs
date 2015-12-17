using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AppConfig.Api.Models;
using AppConfig.Client.ViewModels;


namespace AppConfig.Api.Controllers {

    public class ConfigController : ApiController {
        readonly CfgDbContext data;


        public ConfigController(CfgDbContext data) {
            this.data = data;
        }


        // ie: https://localhost/config/mwl/1.0.0.0/android
        // ie: https://localhost/config/service/2.0.1/PRODWEB
        [HttpGet]
        [Route("~/{appName}/{appVersion}/{environment}")]
        public async Task<AppConfiguration> Get(string appName, string appVersion, string environment) {
            var audit = this.data.Audits.Add(new Audit {
                AppName = appName,
                Environment = environment,
                IpAddress = this.GetIpAddress(),
                Version = appVersion,
                DateCreated = DateTimeOffset.Now
            });
            var cfg = new AppConfiguration {
                Status = ResponseStatus.Success
            };

            var app = await this.data.Applications.FirstOrDefaultAsync(x => x.AccessKey == appName);

            if (app == null || !this.IsAppSecretGood(app))
                cfg.Status = ResponseStatus.ApplicationInvalid;

            else if (!app.IsActive)
                cfg.Status = ResponseStatus.ApplicationInactive;

            else {
                var ver = Version.Parse(appVersion);
                var csQuery = this.data.ConfigSets.Where(x =>
                    x.App.AccessKey == appName &&
                    x.MinVersion.Major >= ver.Major &&
                    x.MinVersion.Minor >= ver.Minor &&
                    x.MinVersion.Revision >= ver.Revision &&
                    x.MaxVersion.Major <= ver.Major &&
                    x.MaxVersion.Minor <= ver.Minor &&
                    x.MaxVersion.Revision <= ver.Revision
                );

                var env = await this.data.Environments.FirstOrDefaultAsync(x => x.AccessKey == environment);

                if (env != null) {
                    if (!env.IsActive)
                        cfg.Status = ResponseStatus.EnvironmentInactive;
                    else
                        csQuery = csQuery.Where(x => x.EnvId == env.Id || x.EnvId == null);
                }

                if (cfg.Status == ResponseStatus.Success) {
                    var data = await csQuery.ToListAsync();

                    var cfgset = data.FirstOrDefault(x => x.Env == null);
                    if (cfgset != null) {
                        audit.ConfigSet = cfgset;
                        cfgset
                            .Settings
                            .ToList()
                            .ForEach(x => cfg.Settings[x.Key] = x.Value);
                    }

                    cfgset = data.FirstOrDefault(x => x.Env.AccessKey == environment);
                    if (cfgset != null) {
                        audit.ConfigSet = cfgset;
                        cfgset
                            .Settings
                            .ToList()
                            .ForEach(x => cfg.Settings[x.Key] = x.Value);
                    }
                }
            }
            await this.data.SaveChangesAsync();

            return cfg;
        }



        bool IsAppSecretGood(App app) {
            if (String.IsNullOrWhiteSpace(app.ClientSecret))
                return true;

            var key = this.Request.Headers.Authorization?.Scheme;
            if (String.IsNullOrWhiteSpace(key))
                return false;

            if (key != app.ClientSecret)
                return false;

            return true;
        }


        const string HttpContext = "MS_HttpContext";
        const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        string GetIpAddress() {
            if (this.Request.Properties.ContainsKey(HttpContext)) {
                dynamic ctx = this.Request.Properties[HttpContext];
                if (ctx != null)
                    return ctx.Request.UserHostAddress;
            }

            if (this.Request.Properties.ContainsKey(RemoteEndpointMessage)) {
                dynamic remoteEndpoint = this.Request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                    return remoteEndpoint.Address;
            }
            return null;
        }
    }
}
