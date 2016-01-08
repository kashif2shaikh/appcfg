using System;
using System.Diagnostics;
using System.Web.Http.Filters;


namespace AppConfig.Api {

    public class LogExceptionFilter : ExceptionFilterAttribute {

        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            Debug.WriteLine(actionExecutedContext.Exception.ToString());
        }
    }
}
