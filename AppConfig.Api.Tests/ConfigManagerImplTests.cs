using System;
using System.Threading.Tasks;
using AppConfig.Client.ViewModels;
using AppConfig.Core;
using AppConfig.Core.Ef;
using AppConfig.Core.Models;
using Autofac;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class ConfigManagerImplTests {
        static readonly string AppName = typeof(ConfigManagerImplTests).Name.ToLower();
        IContainer container;


        [Test]
        public async Task EndToEndTest() {
            var mgr = this.container.Resolve<IConfigManager>();
            var result = await mgr.Get(AppName, "1.0.0", "none", null, null);
            Assert.IsNotNull(result, "No result");
            Assert.IsNotNull(result.Settings, "No settings");
            Assert.AreEqual(ResponseStatus.Success, result.Status);
            Assert.AreEqual(1, result.Settings.Count);
        }


        [OneTimeSetUp]
        public void OnFixtureStart() {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new EfDataModule());
            this.container = builder.Build();

            using (var db = new CfgDbContext()) {
                var app = db.Applications.Add(new App {
                    Description = "good",
                    AccessKey = AppName,
                    ClientSecret = String.Empty,
                    IsActive = true
                });

                //var env = db.Environments.Add(new Env {
                //    IsActive = true
                //});
                //var cfgEnv = db.ConfigSets.Add(new ConfigSet {
                //    Env = env,
                //    IsActive = true,
                //    MaxVersion = new VersionComponent {
                //        Major = 1
                //    },
                //    MinVersion = new VersionComponent {
                //        Major = 1,
                //        Minor = 9
                //    }
                //});
                var cfgNoEnv = db.ConfigSets.Add(new ConfigSet {
                    App = app,
                    IsActive = true,
                    MaxVersion = new VersionComponent {
                        Major = 1
                    },
                    MinVersion = new VersionComponent {
                        Major = 1,
                        Minor = 9
                    }
                });


                // bad sets
                //db.Applications.Add(new App {
                //    Description = AppName + "-bad",
                //    IsActive = false
                //});

                //db.Environments.Add(new Env {
                //    AccessKey = "good",
                //    IsActive = false
                //});

                // settings
                db.Settings.Add(new Setting {
                    ConfigSet = cfgNoEnv,
                    Key = "1",
                    Value = "noenv"
                });
                //db.Settings.Add(new Setting {
                //    ConfigSet = cfgEnv,
                //    Key = "1",
                //    Value = "env"
                //});

                db.SaveChanges();
            }
        }
    }
}
