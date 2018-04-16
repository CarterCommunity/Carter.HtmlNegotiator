namespace HtmlNegotiator
{
    using Microsoft.AspNetCore.Http;

    public interface IViewLocator
    {
        string GetView(object model, HttpContext httpContext);
    }
}
