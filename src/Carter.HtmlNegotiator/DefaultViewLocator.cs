using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class DefaultViewLocator : IViewLocator
    {
        public string GetView(HttpContext httpContext)
        {
            var viewName = "Features/Home/Index.hbs";
            var env = httpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            return File.ReadAllText(Path.Combine(env.ContentRootPath, viewName));
            
        }
    }
}
