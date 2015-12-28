using System;
using System.Threading.Tasks;
using AppConfig.Core.Models;
using AppConfig.Client.ViewModels;
using AppConfig.Core.Ef;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class AppTests : AbstractTests {

        [Test]
        public async Task SuccessTest() {
            var cfg = await this.GetConfig("app", "1.0", "test");
            Assert.AreNotEqual(ResponseStatus.ApplicationInactive, cfg.Status);
            Assert.AreNotEqual(ResponseStatus.ApplicationInvalid, cfg.Status);
        }


        [Test]
        public async Task InvalidClientSecret() {

        }


        [Test]
        public async Task ValidClientSecret() {

        }


        [Test]
        public async Task UnknownTest() {
            var cfg = await this.GetConfig("unknown", "1.0.0.0", "fail");
            Assert.AreEqual(ResponseStatus.ApplicationInvalid, cfg.Status);
        }


        [Test]
        public async Task InactiveTest() {
            var cfg = await this.GetConfig("invalid", "1.0.0.0", "test");
            Assert.AreEqual(ResponseStatus.ApplicationInactive, cfg.Status);
        }


        [SetUp]
        public void OnStart() {
            using (var db = new CfgDbContext()) {
                db.Applications.Add(new App {
                    AccessKey = "app",
                    ClientSecret = String.Empty,
                    Description = "app test",
                    IsActive = true
                });
                db.Applications.Add(new App {
                    AccessKey = "invalid",
                    ClientSecret = String.Empty,
                    Description = "invalid test",
                    IsActive = false
                });
                db.SaveChanges();
            }
        }
    }
}
