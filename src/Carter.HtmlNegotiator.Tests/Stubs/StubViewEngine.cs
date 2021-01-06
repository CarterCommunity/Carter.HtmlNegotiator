namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubViewEngine : IViewEngine
    {
        public string Compile(string source, object model)
        {
            return string.Format(source, model);
        }
    }
}