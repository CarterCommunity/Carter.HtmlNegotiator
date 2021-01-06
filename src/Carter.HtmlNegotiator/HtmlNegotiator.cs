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
            var template = viewLocator.GetView(req.HttpContext);
            var html = viewEngine.Compile(template, model);
            
            res.ContentType = "text/html";
            res.StatusCode = (int)HttpStatusCode.OK;
            await res.WriteAsync(html, cancellationToken: cancellationToken);
        }
    }
}
