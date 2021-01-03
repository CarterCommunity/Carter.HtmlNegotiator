using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Carter.HtmlNegotiator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHtmlNegotiator(this IServiceCollection services)
        {
            services.AddScoped<IViewLocator>(p => new DefaultViewLocator(new Dictionary<Type, string>()));
            services.AddScoped<IResponseNegotiator, HtmlNegotiator>();
        }
    }
}