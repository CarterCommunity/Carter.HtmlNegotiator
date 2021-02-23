using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public static class HttpResponseExtensions
    {
        public static HttpResponse WithView(this HttpResponse response, string viewPath)
        {
            response.HttpContext.Items.Add(Constants.ViewNameKey, viewPath);
            return response;
        }
    }
}