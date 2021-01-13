using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewNameResolver
    {
        public string Resolve(HttpContext context, string defaultViewName, string extension)
        {
            context.Items.TryGetValue(Constants.ViewNameKey, out var viewName);

            viewName ??= GetViewFromPath(context.Request.Path);

            viewName ??= defaultViewName;
            
            return $"{viewName}.{extension}";
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