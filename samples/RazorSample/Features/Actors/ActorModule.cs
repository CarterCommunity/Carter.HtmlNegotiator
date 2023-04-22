using Carter;
using Carter.Response;

namespace RazorSample.Slices;

public class ActorModule : CarterModule
{
    public ActorModule() : base("/actors")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (HttpResponse res) => res.Negotiate(new Person { Name = "Brad Pitt" }));
        app.MapGet("/george", (HttpResponse res) => res.Negotiate(new Person { Name = "George Clooney" }));
    }
}