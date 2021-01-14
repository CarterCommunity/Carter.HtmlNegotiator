using System.Linq;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubFileSystem : IFileSystem
    {
        private readonly string[] filePaths;

        public StubFileSystem(string[] filePaths)
        {
            this.filePaths = filePaths;
        }
        
        public bool FileExists(string path)
        {
            return filePaths.Contains(path);
        }
    }
}