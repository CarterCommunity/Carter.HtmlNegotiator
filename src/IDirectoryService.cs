namespace Carter.HtmlNegotiator
{
    using System.Collections.Generic;

    public interface IDirectoryService
    {
        IEnumerable<ViewTemplate> GetViews(string path, string viewname, IEnumerable<string> extensions);
    }
}