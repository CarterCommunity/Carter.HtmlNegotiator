using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class HandlebarsViewEngine : IViewEngine
    {
        private readonly IHandlebars handlebars;
        private readonly IViewResolver viewResolver;
        private readonly ViewNameResolver viewNameResolver;
        private readonly HtmlNegotiatorConfiguration configuration;
        

        public HandlebarsViewEngine(IHandlebars handlebars, IViewResolver viewResolver,
            ViewNameResolver viewNameResolver, HtmlNegotiatorConfiguration configuration)
        {
            this.handlebars = handlebars;            
            this.viewResolver = viewResolver;
            this.viewNameResolver = viewNameResolver;
            this.configuration = configuration;
        }
        
        public string Extension => "hbs";

        public string GetView(HttpContext httpContext, object model)
        {
            var layout = viewResolver.GetView(httpContext, $"{configuration.DefaultLayoutName}.{Extension}");
            var view = viewResolver.GetView(httpContext, viewNameResolver.Resolve(httpContext, Extension));
            var template = handlebars.Compile(string.Concat(view, layout));
            
            return template(model);
        }
    }
}