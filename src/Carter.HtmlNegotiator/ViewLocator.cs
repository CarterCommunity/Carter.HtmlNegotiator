using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewLocator : IViewLocator
    {
        private readonly HtmlNegotiatorConfiguration configuration;

        public ViewLocator(HtmlNegotiatorConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public LocateViewResult GetView(HttpContext httpContext, string viewName)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }
            
            foreach (var viewLocation in this.configuration.ViewLocationConventions)
            {
                var path = viewLocation;
                var env = httpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
                var allText = File.ReadAllText(Path.Combine(env.ContentRootPath, path));
                return LocateViewResult.Found(viewName, allText);
            }
            return LocateViewResult.NotFound(viewName, new List<string>());
        }
    }
}
