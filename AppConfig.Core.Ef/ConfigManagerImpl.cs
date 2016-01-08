using System;
using System.CodeDom;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AppConfig.Core.Models;
using AppConfig.Client.ViewModels;


namespace AppConfig.Core.Ef {

    public class ConfigManagerImpl : IConfigManager {
        readonly CfgDbContext data;


        public ConfigManagerImpl(CfgDbContext data) {
            this.data = data;
        }


        public async Task<AppConfiguration> Get(string appName, string appVersion, string environment, string secret, string ipAddress) {
            var audit = this.data.Audits.Add(new Audit {
                AppName = appName,
                Environment = environment,
                IpAddress = ipAddress ?? String.Empty,
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
                    x.AppId == app.Id &&
                    x.MinVersion.Major >= ver.Major &&
                    x.MinVersion.Minor >= ver.Minor &&
                    x.MinVersion.Revision >= ver.Build &&
                    x.MaxVersion.Major <= ver.Major &&
                    x.MaxVersion.Minor <= ver.Minor &&
                    x.MaxVersion.Revision <= ver.Build
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
                    if (data.Count == 0)
                        cfg.Status = ResponseStatus.VersionInvalid;
                    else {
                        var cfgset = data.FirstOrDefault(x => x.Env == null);
                        if (cfgset != null) {
                            audit.ConfigSet = cfgset;
                            cfgset
                                .Settings
                                .ToList()
                                .ForEach(x => cfg.Settings[x.Key] = x.Value);
                        }

                        cfgset = data.FirstOrDefault(x => x.Env != null && x.Env.AccessKey == environment);
                        if (cfgset != null) {
                            audit.ConfigSet = cfgset;
                            cfgset
                                .Settings
                                .ToList()
                                .ForEach(x => cfg.Settings[x.Key] = x.Value);
                        }
                    }
                }
            }
            await this.data.SaveChangesAsync();

            return cfg;
        }


        bool IsAppSecretGood(App app) {
            if (String.IsNullOrWhiteSpace(app.ClientSecret))
                return true;

            //var key = this.Request.Headers.Authorization?.Scheme;
            //if (String.IsNullOrWhiteSpace(key))
            //    return false;

            //if (key != app.ClientSecret)
            //    return false;

            return true;
        }
    }
}
