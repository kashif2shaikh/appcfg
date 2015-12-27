using System;
using System.Threading.Tasks;
using System.Web.Http;
using AppConfig.Client.ViewModels;
using AppConfig.Data;


namespace AppConfig.Api.Controllers {

    public class ConfigController : ApiController {
        readonly IConfigManager manager;


        public ConfigController(IConfigManager manager) {
            this.manager = manager;
        }


        // ie: https://localhost/config/mwl/1.0.0.0/android
        // ie: https://localhost/config/service/2.0.1/PRODWEB
        [HttpGet]
        [Route("~/{appName}/{appVersion}/{environment}")]
        public Task<AppConfiguration> Get(string appName, string appVersion, string environment) {
            return this.manager.Get(appName, appVersion, environment, null);
        }





        //const string HttpContext = "MS_HttpContext";
        //const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        //string GetIpAddress() {
        //    if (this.Request.Properties.ContainsKey(HttpContext)) {
        //        dynamic ctx = this.Request.Properties[HttpContext];
        //        if (ctx != null)
        //            return ctx.Request.UserHostAddress;
        //    }

        //    if (this.Request.Properties.ContainsKey(RemoteEndpointMessage)) {
        //        dynamic remoteEndpoint = this.Request.Properties[RemoteEndpointMessage];
        //        if (remoteEndpoint != null)
        //            return remoteEndpoint.Address;
        //    }
        //    return null;
        //}
    }
}
