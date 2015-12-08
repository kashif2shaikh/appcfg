using System;
using System.Data.Entity;
using System.Linq;
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


        // TODO: version range
        // TODO: environment - PRODWEB5 or Android or iOS, etc
        // TODO: environment is not required
        [HttpGet]
        [Route("~/{appName}/{appVersion}/{environment}")]
        public async Task<AppConfiguration> Get(string appName, string appVersion, string environment) {
            this.data.Audits.Add(new Audit {
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

            if (app == null)
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
                        csQuery = csQuery.Where(x => x.EnvId == env.Id);
                }

                if (cfg.Status == ResponseStatus.Success) {
                    var data = await csQuery.ToListAsync();

                }
            }
            await this.data.SaveChangesAsync();

            return cfg;
        }


        string GetIpAddress() {
            return String.Empty;
        }
    }
}
