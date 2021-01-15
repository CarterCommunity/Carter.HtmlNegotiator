using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HandlebarsDotNet;

namespace Carter.HtmlNegotiator
{
    public class HandlebarsViewEngine : IViewEngine
    {
        private IHandlebars handlebars;

        public HandlebarsViewEngine()
        {
            handlebars = Handlebars.Create(new HandlebarsConfiguration
            {
                PartialTemplateResolver = new CustomPartialResolver()
            });
        }
        
        public string Extension => "hbs";

        public string Compile(string viewLocation, object model)
        {
            var layout = GetLayout();
            var page = File.ReadAllText(viewLocation, Encoding.UTF8);
            var template = handlebars.Compile(page + layout);
            return template(model);
        }

        private string GetLayout()
        {
            var layoutPath = "Shared/Layout";
            var files = new List<FileInfo>();

            if (Directory.Exists(layoutPath))
            {
                files.AddRange(GetFiles(layoutPath, Extension));
            }
            return File.ReadAllText(files.First().FullName);
        }
        
        private static FileInfo[] GetFiles(string directoryPath, string desiredExtension)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles($"*.{desiredExtension}");
        }
    }
    
    public class CustomPartialResolver : IPartialTemplateResolver
    {
        private IEnumerable<string> partialPaths = new[]
        {
            "Shared",
            "Views/Shared"
        };
        
        public bool TryRegisterPartial(IHandlebars env, string partialName, string templatePath)
        {
            var partials = new List<string>();
            
            foreach (var path in partialPaths)
            {
                var combine = Path.Combine(path, $"{partialName}.hbs");
                if (File.Exists(combine))
                {
                    partials.Add(combine);
                }
            }

            if (!partials.Any())
            {
                return false;
            }
            
            foreach (var p in partials)
            {
                var template = File.ReadAllText(p, Encoding.UTF8);
                env.RegisterTemplate(partialName, template);
            }
            return true;
        }
    }
}