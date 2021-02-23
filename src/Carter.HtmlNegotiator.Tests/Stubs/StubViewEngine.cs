using System;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewEngine : IViewEngine
    {
        private readonly bool viewNotFound;

        public StubViewEngine(bool viewNotFound = false)
        {
            this.viewNotFound = viewNotFound;
        }
        
        public string Extension => "hbs";

        public string GetView(HttpContext httpContext, object model)
        {
            if (viewNotFound)
            {
                var viewName = httpContext.Request.Path.ToString().TrimStart('/');
                throw new ViewNotFoundException($"{viewName}.hbs", Array.Empty<string>());
            }

            return $"<!doctype html><html><body><h1>{model}</h1></body></html>";
        }
    }
}