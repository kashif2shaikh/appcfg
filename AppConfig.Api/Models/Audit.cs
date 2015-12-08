using System;


namespace AppConfig.Api.Models {

    public class Audit {

        public virtual long Id { get; set; }
        public virtual string AppName { get; set; }
        public virtual string Environment { get; set; }
        public virtual string Version { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual DateTimeOffset DateCreated { get; set; }

        public virtual int? ConfigSetId { get; set; }
        public virtual ConfigSet ConfigSet { get; set; }
    }
}
