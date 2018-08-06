namespace Carter.HtmlNegotiator.Sample.Modules
{
    using ViewModels;

    public class AnotherModule : CarterModule
    {
        public AnotherModule() : base("/another")
        {
            Get("/",
                async (req, res, routeData) =>
                {
                    await res.AsHtml("index", new MyViewModel{Title = "Hello World!", Message = "Hello from Another Module!"});
                });
        }
    }
}