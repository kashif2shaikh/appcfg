using System;
using System.Collections.Generic;


namespace AppConfig.Core.Models {

    public class App {

        public virtual int Id { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string Description { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual ICollection<ConfigSet> ConfigSets { get; set; }
    }
}
