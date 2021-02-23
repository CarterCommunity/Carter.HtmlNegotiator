using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Carter.HtmlNegotiator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHtmlNegotiator(this IServiceCollection services)
        {
            services.AddScoped(p => new HtmlNegotiatorConfiguration(new[]
            {
                "Views/{View}",
                "Views/Shared/{View}",
                "Views/{Resource}/{View}",
                "Features/{View}",
                "Features/Shared/{View}",
                "Features/{Resource}/{View}"
            }));
            
            services.AddScoped<ViewNameResolver>();
            services.AddScoped<IViewResolver, ViewResolver>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IPartialTemplateResolver, PartialTemplateResolver>();

            services.AddScoped(p => Handlebars.Create(new HandlebarsConfiguration
            {
                PartialTemplateResolver = p.GetService<IPartialTemplateResolver>()
            }));
            
            services.AddScoped<IViewEngine, HandlebarsViewEngine>();
            services.AddScoped<IResponseNegotiator, HtmlNegotiator>();
        }
    }
}