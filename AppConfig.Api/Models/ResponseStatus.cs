using System;


namespace AppConfig.Api.Models {

    public enum ResponseStatus {
        UnknownApplication = 1,
        InvalidVersion = 2,
        VersionTooOld = 3,
        InvalidEnvironment = 4, // deactivate an environment?
        Success = 10
    }
}
