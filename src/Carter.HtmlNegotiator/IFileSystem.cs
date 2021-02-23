namespace Carter.HtmlNegotiator
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        string ReadFileContents(string path);
    }
}