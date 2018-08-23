namespace Carter.HtmlNegotiator
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Carter;
    using Carter.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;

    public static class ResponseExtensions
    {
        public static async Task Negotiate(this HttpResponse response, string view, object obj, CancellationToken cancellationToken = default)
        {
            MediaTypeHeaderValue.TryParseList(response.HttpContext.Request.Headers["Accept"], out var accept);
            if (accept != null)
            {
                var ordered = accept.OrderByDescending(x => x.Quality ?? 1);

                var first = ordered.First();

                if (first.SubType == "html")
                {
                    await response.AsHtml(view, obj, cancellationToken);
                    return;
                }
            }

            await response.Negotiate(obj, cancellationToken);
        }
        
        public static async Task AsHtml(this HttpResponse response, string view, object obj, CancellationToken cancellationToken = default)
        {
            response.HttpContext.Items.Add("View", view);
            var negotiators = response.HttpContext.RequestServices.GetServices<IResponseNegotiator>();
            var negotiator = negotiators.FirstOrDefault(x => x.CanHandle(new MediaTypeHeaderValue("text/html")));
            await negotiator.Handle(response.HttpContext.Request, response, obj, cancellationToken);
        }
    }
}
