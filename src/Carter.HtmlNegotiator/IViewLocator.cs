using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public interface IViewLocator
    {
        string GetViewLocation(HttpContext httpContext, IEnumerable<string> locationConventions,
            string rootResourceName, string viewName);
    }
}
