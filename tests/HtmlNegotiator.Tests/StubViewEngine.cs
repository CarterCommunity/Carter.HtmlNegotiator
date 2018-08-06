namespace HtmlNegotiator.Tests
{
    using System.Collections.Generic;
    using Carter.HtmlNegotiator;

    public class StubViewEngine : IViewEngine
    {
        public IEnumerable<string> SupportedExtensions => new[] { "hbs", "html" };

        public string Render(ViewTemplate viewTemplate, object model)
        {
            throw new System.NotImplementedException();
        }
    }
}