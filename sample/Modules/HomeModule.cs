namespace Carter.HtmlNegotiator.Sample.Modules
{
    using Carter;
    using ViewModels;

    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            this.Get("/",
                async (req, res, routeData) =>
                {
                    await res.Negotiate("index",new MyViewModel{Title = "Hello World!", Message = "Hello from Carter.HtmlNegotiator!"});
                });

            this.Get("/custom",
                async (req, res, routeData) =>
                {
                    await res.AsHtml("myView", new MyViewModel{Title = "Custom View!", Message = "Hello from a custom view name!"});
                });
        }
    }
}
