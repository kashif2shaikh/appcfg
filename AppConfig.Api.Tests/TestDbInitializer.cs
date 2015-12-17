using System;
using System.Data.Entity;


namespace AppConfig.Api.Tests {

    public class TestDbInitializer : DropCreateDatabaseAlways<CfgDbContext> {

        protected override void Seed(CfgDbContext db) {}
    }
}
