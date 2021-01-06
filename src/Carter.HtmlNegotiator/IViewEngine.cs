namespace Carter.HtmlNegotiator
{
    public interface IViewEngine
    {
        string Compile(string source, object model);
    }
}