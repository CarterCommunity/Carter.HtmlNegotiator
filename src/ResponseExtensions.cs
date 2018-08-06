namespace Carter.HtmlNegotiator
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Carter;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;

    public static class ResponseExtensions
    {
        public static async Task AsHtml(this HttpResponse response, string view, object obj, CancellationToken cancellationToken = default)
        {
            response.HttpContext.Items.Add("View", view);
            var negotiators = response.HttpContext.RequestServices.GetServices<IResponseNegotiator>();
            var negotiator = negotiators.FirstOrDefault(x => x.CanHandle(new MediaTypeHeaderValue("text/html")));
            await negotiator.Handle(response.HttpContext.Request, response, obj, cancellationToken);
        }
    }
}