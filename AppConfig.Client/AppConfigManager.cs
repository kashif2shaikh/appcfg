using System;
using System.Diagnostics;
using System.Threading;
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
        CancellationTokenSource cancelSrc;


        public AppConfigManager(string appName, string baseUrl, string environment = null, string clientSecret = null) {
            this.appName = appName;
            this.environment = environment ?? DeviceInfo.Hardware.OperatingSystem;
            this.client = new AppConfigClient(baseUrl, clientSecret);
            this.version = Version.Parse(DeviceInfo.App.Version);
        }


        public virtual void StartAutoRefresh(TimeSpan refreshInterval) {
            if (this.cancelSrc != null)
                return;

            this.cancelSrc = new CancellationTokenSource();
            Task.Factory.StartNew(async () => {
                while (!this.cancelSrc.IsCancellationRequested) {
                    await Task.Delay(refreshInterval, this.cancelSrc.Token);
                    await this.OnAutoRefresh();
                }
            }, TaskCreationOptions.LongRunning);
        }


        public virtual void StopAutoRefresh() {
            this.cancelSrc?.Cancel();
            this.cancelSrc = null;
        }


        public virtual Task Initialize() {
            return this.Refresh();
        }


        public virtual async Task Refresh() {
            var cfg = await this.client.GetConfiguration(this.appName, this.version, this.environment);
            if (cfg.Status != ResponseStatus.Success)
                throw new ArgumentException($"Configuration Issue: {cfg.Status}");

            foreach (var setting in cfg.Settings)
                Settings.Local.Set(setting.Key, setting.Value);
        }


        protected virtual async Task OnAutoRefresh() {
            try {
                await this.Refresh();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }
    }
}
