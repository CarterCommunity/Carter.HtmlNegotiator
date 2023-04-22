using Carter;
using Carter.HtmlNegotiator;
using Carter.Response;

namespace RazorSample;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (HttpResponse res) => res.Negotiate(new Person {Name = "Jon"}));
        app.MapGet("/test", (HttpResponse res) => res.Negotiate(new Person {Name = "Test"}));
        app.MapGet("/actors/matt", (HttpResponse res) => res.Negotiate(new Person {Name = "Matt"}));
        app.MapGet("/viewName", (HttpResponse res) => res.WithView("/Foo/Foo.cshtml").Negotiate(new Person {Name = "My View"}));
    }
}

public class Person
{
    public string Name { get; init; } = string.Empty;
}