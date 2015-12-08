using System;


namespace AppConfig.Api.Models {

    public class Setting {

        public virtual int Id { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }

        public virtual int ConfigSetId { get; set; }
        public virtual ConfigSet ConfigSet { get; set; }
    }
}
