using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppConfig.Core.Models;
using AppConfig.Client.ViewModels;
using AppConfig.Core.Ef;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class VersionTests : AbstractTests {
        const string APP = "versiontests";


        [Test]
        public async Task SuccessTest() {
            var cfg = await this.GetConfig(APP, "1.5.0.5", "asdf");
            Assert.AreEqual(ResponseStatus.Success, cfg.Status);
        }


        [Test]
        public async Task VersionInactiveTest() {
            var cfg = await this.GetConfig(APP, "0.1", "asdf");
            Assert.AreEqual(ResponseStatus.VersionInactive, cfg.Status);
        }


        [Test]
        public async Task InvalidTest() {
            var cfg = await this.GetConfig(APP, "99.0", "asdf");
            Assert.AreEqual(ResponseStatus.VersionInvalid, cfg.Status);
        }


        [SetUp]
        public void OnStart() {
            using (var db = new CfgDbContext()) {
                var app = db.Applications.Add(new App {
                    AccessKey = "versiontests",
                    ClientSecret = String.Empty,
                    Description = "version tests",
                    IsActive = true,
                    ConfigSets = new List<ConfigSet>()
                });
                app.ConfigSets.Add(new ConfigSet {
                    App = app,
                    IsActive = false,
                    MinVersion = new VersionComponent {
                        Major = 0,
                        Minor = 0,
                        Revision = 0
                    },
                    MaxVersion = new VersionComponent {
                        Major = 0,
                        Minor = 9,
                        Revision = 9
                    }
                });
                app.ConfigSets.Add(new ConfigSet {
                    App = app,
                    IsActive = true,

                    MinVersion = new VersionComponent {
                        Major = 1,
                        Minor = 0,
                        Revision = 0
                    },
                    MaxVersion = new VersionComponent {
                        Major = 2,
                        Minor = 0,
                        Revision = 0
                    }
                });
                db.SaveChanges();
            }
        }
    }
}
