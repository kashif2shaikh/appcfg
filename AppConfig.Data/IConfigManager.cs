using System;
using System.Threading.Tasks;
using AppConfig.Client.ViewModels;


namespace AppConfig.Data {

    public interface IConfigManager {

        Task<AppConfiguration> Get(string appName, string appVersion, string environment, string secret);
    }
}
