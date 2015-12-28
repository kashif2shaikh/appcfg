using System;
using System.Data.Entity;
using AppConfig.Core.Ef;


namespace AppConfig.Api.Tests {

    public class TestDbInitializer : DropCreateDatabaseAlways<CfgDbContext> {

        protected override void Seed(CfgDbContext db) {}
    }
}
