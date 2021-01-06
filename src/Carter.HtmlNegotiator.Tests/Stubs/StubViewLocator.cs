using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewLocator : IViewLocator
    {
        public string GetView(HttpContext httpContext)
        {
            return "<!doctype html><html><body><h1>{0}</h1></body></html>";
        }
    }
}