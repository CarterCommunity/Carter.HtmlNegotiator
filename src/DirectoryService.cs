namespace Carter.HtmlNegotiator
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<ViewTemplate> GetViews(string path, string viewname, IEnumerable<string> extensions)
        {
            var files = Directory.GetFiles(path, $"{viewname}.*", SearchOption.TopDirectoryOnly);
            return files
                .Where(filename => this.IsValidExtension(filename, extensions))
                .Select(file => new ViewTemplate
                {
                    Name = viewname,
                    Location = path,
                    Extension = Path.GetExtension(file),
                    Source = () => File.ReadAllText(file)
                }).ToList();
        }
        
        private bool IsValidExtension(string filename, IEnumerable<string> supportedExtensions)
        {
            var extension = Path.GetExtension(filename);
            return !string.IsNullOrEmpty(extension) 
                   && supportedExtensions.Contains(extension.TrimStart('.'));
        }
    }
}