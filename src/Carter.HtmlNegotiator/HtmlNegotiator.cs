using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiator : IResponseNegotiator
    {
        private readonly ViewNameResolver viewNameResolver;
        private readonly IViewLocator viewLocator;
        private readonly IViewEngine viewEngine;
        private readonly HtmlNegotiatorConfiguration configuration;

        private string notFoundError => @"The view '{0}' was not found. The following locations were searched:
    {1}";

        public HtmlNegotiator(ViewNameResolver viewNameResolver, IViewLocator viewLocator, IViewEngine viewEngine, HtmlNegotiatorConfiguration configuration)
        {
            this.viewNameResolver = viewNameResolver;
            this.viewLocator = viewLocator;
            this.viewEngine = viewEngine;
            this.configuration = configuration;
        }

        public bool CanHandle(MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            var viewName = this.viewNameResolver.Resolve(req.HttpContext, configuration.DefaultViewName, viewEngine.Extension);
            var viewLocation = viewLocator.GetViewLocation(req.HttpContext, configuration.ViewLocationConventions, configuration.RootResourceName, viewName);
            
            if (!string.IsNullOrEmpty(viewLocation))
            {
                var html = viewEngine.Compile(viewLocation, model);
                res.ContentType = "text/html";
                res.StatusCode = (int)HttpStatusCode.OK;
                await res.WriteAsync(html, cancellationToken: cancellationToken);
            }
            else
            {
                res.ContentType = "text/plain";
                res.StatusCode = (int)HttpStatusCode.InternalServerError;
                await res.WriteAsync(string.Format(notFoundError, viewName, string.Empty), cancellationToken: cancellationToken);
            }
        }
    }
}
