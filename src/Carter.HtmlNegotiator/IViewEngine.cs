using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewEngine
    {
        string Extension { get; }
        
        string GetView(HttpContext httpContext, object model);
    }
}