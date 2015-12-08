using System;


namespace AppConfig.Client.ViewModels {

    public enum ResponseStatus {
        Success = 1,

        ApplicationInvalid = 10,
        ApplicationInactive = 11,

        EnvironmentInactive = 20,

        VersionInvalid = 30,
        VersionInactive = 31
    }
}
