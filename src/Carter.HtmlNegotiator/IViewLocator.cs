using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewLocator
    {
        string GetView(HttpContext httpContext);
    }
}
