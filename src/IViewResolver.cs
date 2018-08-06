namespace Carter.HtmlNegotiator
{
    public interface IViewResolver
    {
        ViewTemplate ResolveView(ViewLocationContext viewLocationContext);
    }
}