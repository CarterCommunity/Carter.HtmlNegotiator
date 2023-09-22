using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Extensions;

namespace Carter.HtmlNegotiator.Razor;

public class RazorResponseNegotiator : IResponseNegotiator
{
    public bool CanHandle(MediaTypeHeaderValue accept)
    {
        return accept.MediaType.Equals("text/html");
    }

    public async Task Handle<T>(HttpRequest req, HttpResponse res, T model, CancellationToken cancellationToken)
    {
        if (!res.HttpContext.Items.TryGetValue("viewName", out var viewName))
        {
            var path = req.Path.ToString();
            var urlSegments = new Uri(req.GetDisplayUrl()).Segments;
            viewName = urlSegments.Length switch
            {
                1 => "Index.cshtml",
                2 => $"/{urlSegments[1]}/Index.cshtml",
                _ => $"{path}.cshtml"
            };
            
            var split = viewName.ToString()!.Split("/", StringSplitOptions.RemoveEmptyEntries);
            var viewLocation = "/Slices";
            foreach (var s in split)
            {
                viewLocation += "/" + s[0].ToString().ToUpper() + s[Range.StartAt(1)];
            }
            
            var result = Results.Extensions.RazorSlice(viewLocation, model, res.StatusCode);
            await result.ExecuteAsync(res.HttpContext);
        }
        else
        {
            var viewLocation = "/Slices";
            var result = Results.Extensions.RazorSlice(viewLocation + viewName!, model, res.StatusCode);
            await result.ExecuteAsync(res.HttpContext);
        }
       

        
        

        
    }
}