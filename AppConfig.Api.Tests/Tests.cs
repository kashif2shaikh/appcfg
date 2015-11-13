using System;
using System.Threading.Tasks;
using Acr.Settings;
using AppConfig.Client;
using AppConfig.Model;
using Microsoft.Owin.Hosting;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class Tests {
        const string URL = "http://localhost:6789";
        ISettings settings;


        [SetUp]
        public void OnBeforeTest() {
            Settings.Local = new InMemorySettingsImpl();
            this.settings = Settings.Local;
        }


        [Test]
        public async Task SuccessTest() {
            this.SetDefaultAppAndVersion();
            var cfg = await GetConfig();

            Assert.AreEqual(ConfigStatus.Success, cfg.Status);
            Assert.AreEqual("test", cfg.BaseApiUrl);
        }


        [Test]
        public async Task CustomParametersTest() {
            this.SetDefaultAppAndVersion();
            this.settings.Set("test/1.0.0.0/custom1", "1");
            this.settings.Set("test/1.0.0.0/custom2", "2");
            var cfg = await GetConfig();
            Assert.AreEqual("1", cfg.CustomParameters["custom1"]);
            Assert.AreEqual("2", cfg.CustomParameters["custom2"]);
        }


        [Test]
        public async Task UnknownAppTest() {
            var cfg = await GetConfig();
            Assert.AreEqual(ConfigStatus.UnknownApp, cfg.Status);
        }


        [Test]
        public async Task UnknownVersionTest() {
            this.SetDefaultAppAndVersion();
            var cfg = await GetConfig("test", "2.0.0.0");
            Assert.AreEqual(ConfigStatus.UnknownVersion, cfg.Status);
        }


        [Test]
        public async Task VersionTooOldTest() {
            throw new NotImplementedException("not done");
        }


        [Test]
        public async Task VersionRangeTest() {
            throw new NotImplementedException("not done");
        }


        void SetDefaultAppAndVersion() {
            this.settings.Set("test/1.0.0.0/url", "test");
        }


        static async Task<AppConfiguration> GetConfig(string appName = "test", string appVersion = "1.0.0.0") {
            var ver = Version.Parse(appVersion);

            using (WebApp.Start<Startup>(URL)) {
                var client = new AppConfigClient(URL);
                return await client.GetConfiguration(appName, ver);
            }
        }
    }
}
