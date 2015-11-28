using System;


namespace AppConfig.Api.Models {

    public class Audit {

        public ResponseStatus Status { get; set; }
        public string IpAddress { get; set; }
        public string Environment { get; set; }
        public Version Version { get; set; }
        public string ApplicationName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
