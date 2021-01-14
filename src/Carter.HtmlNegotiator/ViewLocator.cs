using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewLocator : IViewLocator
    {
        private readonly IFileSystem fileSystem;

        public ViewLocator(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public string GetViewLocation(HttpContext httpContext, IEnumerable<string> locationConventions, string rootResourceName, string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
                return null;

            var resource = GetResourceNameFromPath(httpContext.Request.Path);
            resource ??= rootResourceName;

            var locatedViews = new List<string>();
            var searchedLocations = new List<string>();
            
            foreach (var convention in locationConventions)
            {
                var path = convention
                    .Replace("{resource}", resource)
                    .Replace("{view}", viewName);
                
                if (this.fileSystem.FileExists(path))
                {
                    locatedViews.Add(path);
                }
                searchedLocations.Add(path);
            }

            if (!locatedViews.Any())
            {
                throw new ViewNotFoundException(viewName, searchedLocations);
            }

            if (locatedViews.Count > 1)
            {
                throw new AmbiguousViewsException(locatedViews);
            }
            
            return locatedViews.First();
        }

        private string GetResourceNameFromPath(PathString path)
        {
            if (!path.HasValue || path.Value.All(x => x != '/'))
                return null;

            var segments = path.Value.Split("/");

            var resource = segments[1];
            return string.IsNullOrWhiteSpace(resource)
                ? null
                : resource;
        }
    }
}
