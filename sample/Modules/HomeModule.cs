namespace Carter.HtmlNegotiator.Sample.Modules
{
    using Carter;
    using ViewModels;

    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            Get("/",
                async (req, res, routeData) =>
                {
                    await res.AsHtml("index",new MyViewModel{Title = "Hello World!", Message = "Hello from Carter.HtmlNegotiator!"});
                });
            
            Get("/custom",
                async (req, res, routeData) =>
                {
                    await res.AsHtml("myView", new MyViewModel{Title = "Custom View!", Message = "Hello from a custom view name!"});
                });
        }
    }
}
