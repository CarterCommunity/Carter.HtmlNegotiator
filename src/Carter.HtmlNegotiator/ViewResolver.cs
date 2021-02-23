using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Carter.HtmlNegotiator
{
    public class ViewResolver : IViewResolver
    {
        private readonly IFileSystem fileSystem;
        private readonly IWebHostEnvironment env;
        private readonly HtmlNegotiatorConfiguration configuration;

        public ViewResolver(IFileSystem fileSystem, IWebHostEnvironment env, HtmlNegotiatorConfiguration configuration)
        {
            this.fileSystem = fileSystem;
            this.env = env;
            this.configuration = configuration;
        }

        public string GetView(HttpContext httpContext, string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
                return null;

            var resource = GetResourceNameFromPath(httpContext.Request.Path);
            resource ??= configuration.RootResourceName;
            
            if (viewName.StartsWith(resource, StringComparison.InvariantCultureIgnoreCase))
            {
                resource = configuration.RootResourceName;
            }
            
            var locatedViews = new List<string>();
            var searchedLocations = new List<string>();
            
            foreach (var convention in configuration.ViewLocationConventions)
            {
                var path = convention
                    .Replace($"{{{Constants.ResourceNameKey}}}", resource)
                    .Replace($"{{{Constants.ViewNameKey}}}", viewName);

                var fullPath = Path.Combine(env.ContentRootPath, path);
                if (this.fileSystem.FileExists(fullPath))
                {
                    locatedViews.Add(fullPath);
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
            
            return fileSystem.ReadFileContents(locatedViews.First());
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
