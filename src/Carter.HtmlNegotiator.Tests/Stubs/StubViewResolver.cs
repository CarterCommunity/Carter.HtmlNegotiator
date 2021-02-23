using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewResolver : IViewResolver
    {
        private readonly Dictionary<string, string> views;

        public StubViewResolver(Dictionary<string, string> views)
        {
            this.views = views;
        }
        
        public string GetView(HttpContext httpContext, string viewName)
        {
            return this.views.TryGetValue(viewName, out var view)
                ? view
                : null;
        }
    }
}