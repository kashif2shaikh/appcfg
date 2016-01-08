using System;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [SetUpFixture]
    public class StartFixture {

        [OneTimeSetUp]
        public void OnStart() {
            EntityFrameworkProfiler.Initialize();
        }
    }
}
