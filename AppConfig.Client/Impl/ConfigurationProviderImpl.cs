//using System;
//using System.Threading.Tasks;
//using Acr.DeviceInfo;
//using Acr.Settings;


//namespace AppConfig.Client.Impl {

//    public class ConfigurationProviderImpl : IConfigurationProvider {
//        readonly IApp app;
//        readonly ISettings settings;
//        readonly AppConfigClient client;


//        public ConfigurationProviderImpl(IApp app, IHardware hardware, ISettings settings, string uri, int? autoRefreshMins = null) {
//            this.client = new AppConfigClient(uri);
//            this.app = app ?? DeviceInfo.App;
//            this.settings = settings ?? Settings.Local;

//            if (autoRefreshMins != null) {
//                Task.Factory.StartNew(async () => {
//                    var ts = TimeSpan.FromMinutes(autoRefreshMins.Value);

//                    while (true) {
//                        this.client.GetConfiguration("", this.app.Version, hardware.OS.ToString());
//                        await Task.Delay(ts);
//                    }
//                });
//            }
//        }
//    }
//}
