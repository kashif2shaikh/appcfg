using System;
using System.Threading.Tasks;
using AppConfig.Api.Models;
using AppConfig.Client;
using AppConfig.Client.ViewModels;
using Microsoft.Owin.Hosting;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class EnvironmentTests : AbstractTests {

        [Test]
        public async Task SuccessTest() {
        }


        [Test]
        public async Task UnknownTest() {

        }


        [Test]
        public async Task AllDefaultTest() {

        }


        [Test]
        public async Task EnviroSettingFirstTest() {

        }



        [SetUp]
        public void OnStart() {
            using (var db = new CfgDbContext()) {
                db.Applications.Add(new App {
                    AccessKey = "good",
                    ClientSecret = String.Empty,
                    Description = "Good Config",
                    IsActive = true
                });
            }
        }
    }
}
