using System;
using System.Collections;
using System.Collections.Generic;


namespace AppConfig.Api.Models {

    public class ConfigSet {

        public ResponseStatus Status { get; set; }
        public string ApplicationName { get; set; }
        public string Environment { get; set; }
        public Version Minimum { get; set; }
        public Version Maximum { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
