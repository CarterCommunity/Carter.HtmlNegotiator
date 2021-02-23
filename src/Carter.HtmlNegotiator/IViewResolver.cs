using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewResolver
    {
        string GetView(HttpContext httpContext, string viewName);
    }
}
