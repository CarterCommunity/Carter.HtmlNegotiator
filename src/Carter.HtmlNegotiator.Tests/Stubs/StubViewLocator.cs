using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewLocator : IViewLocator
    {
        public string GetViewLocation(HttpContext httpContext, IEnumerable<string> locationConventions, string viewName)
        {
            return viewName != "not-found.hbs" 
                ? "Views/Home/Index.hbs"
                : null;
        }
    }
}