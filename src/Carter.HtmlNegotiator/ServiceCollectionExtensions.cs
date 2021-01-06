using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Carter.HtmlNegotiator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHtmlNegotiator(this IServiceCollection services)
        {
            services.AddScoped<IViewLocator, DefaultViewLocator>();
            services.AddScoped<IViewEngine, HandlebarsViewEngine>();
            services.AddScoped<IResponseNegotiator, HtmlNegotiator>();
        }
    }
}