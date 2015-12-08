using System;
using System.Collections.Generic;


namespace AppConfig.Api.Models {

    public class ConfigSet {

        public virtual int Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual VersionComponent MinVersion { get; set; }
        public virtual VersionComponent MaxVersion { get; set; }

        public virtual int AppId { get; set; }
        public virtual App App { get; set; }

        public virtual int? EnvId { get; set; }
        public virtual Env Env { get; set; }

        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
