namespace Carter.HtmlNegotiator
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Carter;
    using Microsoft.AspNetCore.Http;

    public class HtmlNegotiator : IResponseNegotiator
    {
        private readonly IViewRenderer viewRenderer;

        public HtmlNegotiator(IViewRenderer viewRenderer)
        {
            this.viewRenderer = viewRenderer;
        }

        public bool CanHandle(Microsoft.Net.Http.Headers.MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            var view = this.viewRenderer.RenderView(req.HttpContext, model);
            
            if (string.IsNullOrEmpty(view))
            {
                res.StatusCode = 500;
                res.ContentType = "text/plain";
                await res.WriteAsync("View not found", cancellationToken);
                return;
            }

            res.ContentType = "text/html";
            res.StatusCode = (int)HttpStatusCode.OK;
            await res.WriteAsync(view, cancellationToken);
        }
    }
}
