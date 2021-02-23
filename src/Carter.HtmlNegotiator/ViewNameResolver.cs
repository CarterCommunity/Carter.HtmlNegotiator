using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewNameResolver
    {
        private readonly HtmlNegotiatorConfiguration configuration;

        public ViewNameResolver(HtmlNegotiatorConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public string Resolve(HttpContext context, string extension)
        {
            context.Items.TryGetValue(Constants.ViewNameKey, out var value);

            value ??= GetViewFromPath(context.Request.Path) ?? configuration.DefaultViewName;

            var viewName = value as string;
            return viewName.EndsWith(extension) 
            ? viewName
            : $"{viewName}.{extension}";
        }

        private string GetViewFromPath(PathString path)
        {
            if (!path.HasValue || path.Value.All(x => x != '/'))
                return null;

            var segments = path.Value.Split("/");

            var viewName = segments.Last();
            return string.IsNullOrWhiteSpace(viewName)
                ? null
                : viewName;
        }
    }
}