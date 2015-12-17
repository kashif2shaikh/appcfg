using System;
using System.Threading.Tasks;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [TestFixture]
    public class VersionTests : AbstractTests {

        [Test]
        public async Task SuccessTest() {
        }


        [Test]
        public async Task UnknownVersionTest() {
        }


        [Test]
        public async Task VersionTooOldTest() {
            throw new NotImplementedException("not done");
        }


        [Test]
        public async Task ValidTest() {
            throw new NotImplementedException("not done");
        }


        [Test]
        public async Task InvalidTest() {

        }


        //[SetUp]
        //public void OnStart() {
        //    using (var db = new CfgDbContext()) {
        //        db.Applications.Add(new App {
        //            AccessKey = "good",
        //            ClientSecret = String.Empty,
        //            Description = "Good Config",
        //            IsActive = true
        //        });
        //    }
        //}
    }
}
