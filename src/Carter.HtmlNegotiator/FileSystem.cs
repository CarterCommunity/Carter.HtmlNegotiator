using System.IO;
using System.Text;

namespace Carter.HtmlNegotiator
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadFileContents(string path)
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }

        public string[] GetFilesInDirectory(string path, string extension)
        {
            return Directory.GetFiles(path, $"*.{extension}");
        }
    }
}