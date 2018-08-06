namespace Carter.HtmlNegotiator
{
    using Microsoft.AspNetCore.Http;

    public interface IViewLocator
    {
        ViewTemplate LocateView(string viewLocation);
        //string LocateView(object model, HttpContext httpContext);
    }
}
