using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewLocator
    {
        LocateViewResult GetView(HttpContext httpContext, string viewName);
    }
}
