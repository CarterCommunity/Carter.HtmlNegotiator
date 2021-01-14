using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Carter.HtmlNegotiator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHtmlNegotiator(this IServiceCollection services)
        {
            services.AddScoped<ViewNameResolver>();
            services.AddScoped<IViewLocator, ViewLocator>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IViewEngine, HandlebarsViewEngine>();
            services.AddScoped(p => new HtmlNegotiatorConfiguration(new[]
            {
                "Views/{resource}/{view}",
                "Features/{resource}/{view}",
                "Features/{resource}/Views/{view}"
            }));
            services.AddScoped<IResponseNegotiator, HtmlNegotiator>();
        }
    }
}