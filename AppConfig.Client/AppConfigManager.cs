using System;
using System.Threading.Tasks;
using Acr.DeviceInfo;
using Acr.Settings;
using AppConfig.Client.ViewModels;


namespace AppConfig.Client {

    public class AppConfigManager {
        readonly AppConfigClient client;
        readonly string appName;
        readonly string environment;
        readonly Version version;


        public AppConfigManager(string appName, string baseUrl, string environment = null, string clientSecret = null, TimeSpan? refreshInterval = null) {
            this.appName = appName;
            this.environment = environment ?? DeviceInfo.Hardware.OperatingSystem;
            this.client = new AppConfigClient(baseUrl, clientSecret);
            this.version = Version.Parse(DeviceInfo.App.Version);
            //if (refreshInterval != null)

        }


        public async Task Initialize() {
            var cfg = await this.client.GetConfiguration(this.appName, this.version, this.environment);
            if (cfg.Status != ResponseStatus.Success)
                throw new ArgumentException($"Configuration Issue: {cfg.Status}");

            foreach (var setting in cfg.Settings)
                Settings.Local.Set(setting.Key, setting.Value);
        }
    }
}
