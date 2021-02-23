using System.Collections.Generic;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubFileSystem : IFileSystem
    {
        private readonly IDictionary<string, string> viewTemplates;
        
        public StubFileSystem(IDictionary<string, string> viewTemplates)
        {
            this.viewTemplates = viewTemplates;
        }
        
        public bool FileExists(string path)
        {
            return viewTemplates.ContainsKey(path);
        }

        public string ReadFileContents(string path)
        {
            viewTemplates.TryGetValue(path, out var source);
            return source;
        }
    }
}