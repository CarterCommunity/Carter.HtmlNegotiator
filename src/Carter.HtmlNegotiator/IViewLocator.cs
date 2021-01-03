using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewLocator
    {
        string GetView(object model, HttpContext httpContext);
    }
}
