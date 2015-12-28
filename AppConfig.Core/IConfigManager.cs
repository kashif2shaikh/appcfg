using System;
using System.Threading.Tasks;
using AppConfig.Client.ViewModels;


namespace AppConfig.Core {

    public interface IConfigManager {

        Task<AppConfiguration> Get(string appName, string appVersion, string environment, string secret);
    }
}
