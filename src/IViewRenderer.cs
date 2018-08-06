namespace Carter.HtmlNegotiator
{
    using Microsoft.AspNetCore.Http;

    public interface IViewRenderer
    {
        string RenderView(HttpContext httpContext, object model);
    }
}