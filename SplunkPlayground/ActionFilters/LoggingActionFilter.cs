using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using Autofac.Integration.WebApi;
using Serilog;

namespace SplunkPlayground.ActionFilters
{
    public class LoggingActionFilter : IAutofacActionFilter
    {
        private readonly ILogger _logger;

        public LoggingActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    _logger.Information("Request: {@request}", filterContext.Request);
        //    //var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        //    //trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
        //}

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //_logger.Information("Request: {@request}", actionExecutedContext.Request);
            var request = actionExecutedContext.Request;
            _logger.Information("{@requestUri} {@requestMethod} {@requestContent}", request.RequestUri, request.Method, request.Content.ReadAsStringAsync().Result);
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var request = actionContext.Request;
            return Task.FromResult(0);
        }
    }  
}