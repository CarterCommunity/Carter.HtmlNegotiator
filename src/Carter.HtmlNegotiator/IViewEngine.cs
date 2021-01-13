namespace Carter.HtmlNegotiator
{
    public interface IViewEngine
    {
        string Extension { get; }
        
        string Compile(string viewLocation, object model);
    }
}