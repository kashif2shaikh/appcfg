using System;
using System.Web.Http.Filters;


namespace AppConfig.Api.Tests {

    public class ConsoleExceptionFilterAttribute : ExceptionFilterAttribute {

        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            base.OnException(actionExecutedContext);
            Console.WriteLine(actionExecutedContext.Exception.ToString());
        }
    }
}
