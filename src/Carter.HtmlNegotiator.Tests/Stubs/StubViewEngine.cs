namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewEngine : IViewEngine
    {
        public string Extension => "hbs";

        public string Compile(string viewLocation, object model)
        {
            return $"<!doctype html><html><body><h1>{model}</h1></body></html>";
        }
    }
}