using System;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using NUnit.Framework;


namespace AppConfig.Api.Tests {

    [SetUpFixture]
    public class Start {

        [SetUp]
        public void OnStart() {
            EntityFrameworkProfiler.Initialize();
        }
    }
}
