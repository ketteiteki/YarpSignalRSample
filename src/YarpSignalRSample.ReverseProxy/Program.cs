using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

var reverseProxySection = builder.Configuration.GetSection("ReverseProxy");

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(reverseProxySection)
    .AddTransforms(transformBuilderContext =>
    {
        transformBuilderContext.AddRequestTransform(async transformContext =>
        {
            if (transformContext.Path.StartsWithSegments("/notify"))
            {
                var token = transformContext.HttpContext.Request.Headers
                    .First(x => x.Key == "Authorization").Value.ToString().Split(' ')[1];
                transformContext.Query.Collection.Add("access_token", token);   
            }
        });
    });;

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapReverseProxy();

app.Run();
