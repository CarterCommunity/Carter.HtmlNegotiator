using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewLocator : IViewLocator
    {
        public LocateViewResult GetView(HttpContext httpContext, string viewName)
        {
            return httpContext.Request.Path.HasValue 
                ? LocateViewResult.NotFound(viewName, new List<string>()) 
                : LocateViewResult.Found(viewName, "<!doctype html><html><body><h1>{0}</h1></body></html>");
        }
    }
}