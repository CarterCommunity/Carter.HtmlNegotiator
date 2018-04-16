namespace HtmlNegotiator
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Carter;
    using HandlebarsDotNet;
    using Microsoft.AspNetCore.Http;

    public class HtmlNegotiator : IResponseNegotiator
    {
        private readonly IViewLocator viewLocator;

        public HtmlNegotiator(IViewLocator viewLocator)
        {
            this.viewLocator = viewLocator;
        }

        public bool CanHandle(Microsoft.Net.Http.Headers.MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            var source = viewLocator.GetView(model, res.HttpContext);
            if (string.IsNullOrEmpty(source))
            {
                res.StatusCode = 500;
                res.ContentType = "text/plain";
                await res.WriteAsync("View not found", cancellationToken);
            }

            var template = Handlebars.Compile(source);

            res.ContentType = "text/html";
            res.StatusCode = (int)HttpStatusCode.OK;

            await res.WriteAsync(template(model), cancellationToken);
        }
    }
}
