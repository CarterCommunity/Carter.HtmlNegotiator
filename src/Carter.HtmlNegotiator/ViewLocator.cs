using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewLocator : IViewLocator
    {
        private List<string> mappings;

        public ViewLocator()
        {
            mappings = new List<string>();
        }
        public LocateViewResult GetView(HttpContext httpContext, string viewName)
        {
            var path = $"Features/Home/{viewName}";
            var env = httpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            return LocateViewResult.Found("Index", File.ReadAllText(Path.Combine(env.ContentRootPath, path)));
        }
    }
}
