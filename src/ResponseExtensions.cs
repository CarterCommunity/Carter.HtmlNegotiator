using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator;

public static class ResponseExtensions
{
    public static HttpResponse WithView(this HttpResponse response, string viewPath)
    {
        response.HttpContext.Items["viewName"] = viewPath;
        return response;
    }
}