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
        private readonly IViewEngine viewEngine;

        public HtmlNegotiator(IViewEngine viewEngine)
        {
            this.viewEngine = viewEngine;
        }

        public bool CanHandle(MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            try
            {
                var html = viewEngine.GetView(req.HttpContext, model);
                res.ContentType = "text/html";
                res.StatusCode = (int)HttpStatusCode.OK;
                await res.WriteAsync(html, cancellationToken: cancellationToken);
            }
            catch (ViewNotFoundException e)
            {
                res.ContentType = "text/plain";
                res.StatusCode = (int)HttpStatusCode.NotFound;
                await res.WriteAsync(e.Message, cancellationToken: cancellationToken);
            }
        }
    }
}
