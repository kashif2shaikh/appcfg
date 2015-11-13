using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppConfig.Model;
using Newtonsoft.Json;


namespace AppConfig.Client {

    public class AppConfigClient {
        public string BaseUrl { get; set; }


        public AppConfigClient(string baseUrl) {
            this.BaseUrl = baseUrl;
        }


        public async Task<AppConfiguration> GetConfiguration(string appName, Version appVersion) {
            using (var http = new HttpClient()) {
                var response = await http.GetStringAsync($"{this.BaseUrl}/{appName}/{appVersion}");
                var result = JsonConvert.DeserializeObject<AppConfiguration>(response);
                return result;
            }
        }
    }
}
