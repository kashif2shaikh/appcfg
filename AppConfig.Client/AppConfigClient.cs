using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppConfig.Client.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AppConfig.Client {

    public class AppConfigClient {
        public string BaseUrl { get; set; }
        public string ClientSecret { get; set; }


        public AppConfigClient(string baseUrl, string clientSecret = null) {
            this.BaseUrl = baseUrl;
            this.ClientSecret = clientSecret;
        }


        public async Task<AppConfiguration> GetConfiguration(string appName, Version appVersion, string environment) {
            using (var http = new HttpClient()) {
                if (this.ClientSecret != null)
                    http.DefaultRequestHeaders.Add("s", this.ClientSecret);

                var response = await http.GetStringAsync($"{this.BaseUrl}/{appName}/{appVersion}");
                var result = JsonConvert.DeserializeObject<AppConfiguration>(response);
                return result;
            }
        }


        public async Task<T> GetTypedConfiguration<T>(string appName, Version appVersion, string environment) where T : class {
            var result = await this.GetConfiguration(appName, appVersion, environment);
            if (result.Status != ResponseStatus.Success)
                throw new ArgumentException($"Invalid Configuration Response - {result.Status}");

            var obj = new JObject();
            foreach (var setting in result.Settings)
                obj.Add(setting.Key, setting.Value);

            var ret = obj.ToObject<T>();
            return ret;
        }
    }
}
