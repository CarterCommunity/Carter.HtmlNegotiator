namespace Carter.HtmlNegotiator
{
    using System.Collections.Generic;

    public interface IViewEngine
    {
        IEnumerable<string> SupportedExtensions { get; }
        string Render(ViewTemplate viewTemplate, object model);
    }
}