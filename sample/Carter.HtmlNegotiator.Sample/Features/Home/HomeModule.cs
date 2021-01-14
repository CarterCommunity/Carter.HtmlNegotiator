using Carter.Request;
using Carter.Response;

namespace Carter.HtmlNegotiator.Sample.Features.Home
{
    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            Get("/", (request, response) => response.Negotiate(new {}));

            Get("/echo", (request, response) => response
                .WithView("Echo.hbs")
                .Negotiate(new EchoViewModel
                {
                    Message = request.Query.As<string>("msg")
                })
            );
        }
    }
}