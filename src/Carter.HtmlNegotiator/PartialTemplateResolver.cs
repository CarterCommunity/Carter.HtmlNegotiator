using System.Collections.Generic;
using System.IO;
using System.Linq;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Hosting;

namespace Carter.HtmlNegotiator
{
    public class PartialTemplateResolver : IPartialTemplateResolver
    {
        private readonly HtmlNegotiatorConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly IFileSystem fileSystem;

        public PartialTemplateResolver(HtmlNegotiatorConfiguration configuration, IWebHostEnvironment environment, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.fileSystem = fileSystem;
        }
        
        public bool TryRegisterPartial(IHandlebars env, string partialName, string templatePath)
        {
            var locatedTemplates = new List<string>();
            
            foreach (var convention in configuration.ViewLocationConventions)
            {
                var path = convention
                    .Replace($"{{{Constants.ViewNameKey}}}", $"{partialName}.hbs");
                
                var fullPath = Path.Combine(environment.ContentRootPath, path);
                if (this.fileSystem.FileExists(fullPath))
                {
                    locatedTemplates.Add(fullPath);
                }
            }
            
            if (!locatedTemplates.Any())
            {
                return false;
            }

            if (locatedTemplates.Count > 1)
            {
                throw new AmbiguousViewsException(locatedTemplates);
            }
            
            env.RegisterTemplate(partialName, fileSystem.ReadFileContents(locatedTemplates.First()));
            return true;
        }
    }
}