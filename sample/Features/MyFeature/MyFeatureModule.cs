namespace Carter.HtmlNegotiator.Sample.Features.MyFeature
{
    using ViewModels;

    public class MyFeatureModule : CarterModule
    {
        public MyFeatureModule() : base("/myfeature")
        {
            Get("/",
                async (req, res, routeData) =>
                {
                    await res.AsHtml("index", new MyViewModel{Title = "Hello World!", Message = "Hello from a Custom View Location"});
                });
        }
    }
}