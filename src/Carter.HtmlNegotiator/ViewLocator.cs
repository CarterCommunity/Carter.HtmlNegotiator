using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewLocator : IViewLocator
    {
        public string GetViewLocation(HttpContext httpContext, IEnumerable<string> locationConventions, string viewName)
        {
            return string.Empty;
        }
    }
}
