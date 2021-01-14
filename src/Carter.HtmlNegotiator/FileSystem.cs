using System.IO;

namespace Carter.HtmlNegotiator
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}