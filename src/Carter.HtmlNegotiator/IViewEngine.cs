namespace Carter.HtmlNegotiator
{
    public interface IViewEngine
    {
        string Extension { get; }
        
        string Compile(string source, object model);
    }
}