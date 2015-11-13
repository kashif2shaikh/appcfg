using System;
using System.Collections.Generic;


namespace AppConfig.Model {

    public class AppConfiguration {

        public string ApplicationName { get; set; }
        public Version Version { get; set; }
        public string BaseApiUrl { get; set; }
        public ConfigStatus Status { get; set; }
        public Dictionary<string, string> CustomParameters { get; set; }
    }
}
