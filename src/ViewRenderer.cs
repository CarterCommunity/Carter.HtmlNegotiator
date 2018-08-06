namespace Carter.HtmlNegotiator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class ViewRenderer : IViewRenderer
    {
        private readonly IViewResolver viewResolver;
        private readonly IEnumerable<IViewEngine> viewEngines;

        public ViewRenderer(IViewResolver viewResolver, IEnumerable<IViewEngine> viewEngines)
        {
            this.viewResolver = viewResolver;
            this.viewEngines = viewEngines;
        }

        public string RenderView(HttpContext httpContext, object model)
        {
            var viewTemplate = viewResolver.ResolveView(GetViewLocationContext(httpContext, model));
            if (viewTemplate == null)
            {
                return null;
            }
            var viewEngine = viewEngines.FirstOrDefault(ve => ve.SupportedExtensions.Any(e => e.Equals(viewTemplate.Extension.TrimStart('.'), StringComparison.OrdinalIgnoreCase)));
            return viewEngine.Render(viewTemplate, model);
        }

        private ViewLocationContext GetViewLocationContext(HttpContext httpContext, object model)
        {
            return new ViewLocationContext
            {
                RootPath = ((IHostingEnvironment)httpContext.RequestServices.GetService(typeof(IHostingEnvironment))).ContentRootPath,
                ViewName = GetViewName(httpContext, model),
                ModuleName = GetModuleName(httpContext.Items["ModuleType"] as Type)
            };
        }

        private string GetModuleName(Type moduleType)
        {
            var moduleTypeName = moduleType.ToString().Split('.').Last().ToLower();
            return moduleTypeName.Substring(0, moduleTypeName.IndexOf("module"));
        }

        private string GetViewName(HttpContext httpContext, object model)
        {
            if (!httpContext.Items.ContainsKey("View") && model == null)
            {
                return "index";
            }
            
            if (!httpContext.Items.ContainsKey("View"))
            {
                return Regex.Replace(model.GetType().Name, "ViewModel|Model", string.Empty);
            }

            return httpContext.Items["View"].ToString();
        }
    }
}