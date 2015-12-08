using System;
using System.Threading.Tasks;
using AppConfig.Client;
using Microsoft.Owin.Hosting;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class Tests {
        const string URL = "http://localhost:6789";



        [Test]
        public async Task SuccessTest() {
        }


        [Test]
        public async Task UnknownAppTest() {
        }


        [Test]
        public async Task UnknownEnvironmentTest() {

        }


        [Test]
        public async Task GlobalEnvironmentTest() {

        }


        [Test]
        public async Task SpecificEnvironmentTest() {

        }


        [Test]
        public async Task UnknownVersionTest() {
        }


        [Test]
        public async Task VersionTooOldTest() {
            throw new NotImplementedException("not done");
        }


        [Test]
        public async Task ValidVersionRangeTest() {
            throw new NotImplementedException("not done");
        }


        [Test]
        public async Task InvalidVersionTest() {

        }

        //static async Task<AppConfiguration> GetConfig(string appName = "test", string appVersion = "1.0.0.0") {
        //    var ver = Version.Parse(appVersion);

        //    using (WebApp.Start<Startup>(URL)) {
        //        var client = new AppConfigClient(URL);
        //        return await client.GetConfiguration(appName, ver);
        //    }
        //}
    }
}
