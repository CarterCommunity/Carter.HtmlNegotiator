using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiator : IResponseNegotiator
    {
        private readonly IViewLocator viewLocator;
        private readonly IViewEngine viewEngine;

        private string notFoundError => @"The view '{0}' was not found. The following locations were searched:
    {1}";

        public HtmlNegotiator(IViewLocator viewLocator, IViewEngine viewEngine)
        {
            this.viewLocator = viewLocator;
            this.viewEngine = viewEngine;
        }

        public bool CanHandle(MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            var result = viewLocator.GetView(req.HttpContext, "Index.hbs");
            
            if (result.Success)
            {
                var html = viewEngine.Compile(result.View, model);
                res.ContentType = "text/html";
                res.StatusCode = (int)HttpStatusCode.OK;
                await res.WriteAsync(html, cancellationToken: cancellationToken);
            }
            else
            {
                res.ContentType = "text/plain";
                res.StatusCode = (int)HttpStatusCode.InternalServerError;
                await res.WriteAsync(string.Format(notFoundError, result.ViewName, string.Join(Environment.NewLine, result.SearchedLocations)), cancellationToken: cancellationToken);
            }
        }
    }
}
