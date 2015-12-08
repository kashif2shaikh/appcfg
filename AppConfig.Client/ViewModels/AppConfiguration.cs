using System;
using System.Collections.Generic;


namespace AppConfig.Client.ViewModels {

    public class AppConfiguration {

        public ResponseStatus Status { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
