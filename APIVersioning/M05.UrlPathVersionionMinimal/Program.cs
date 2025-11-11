
using Asp.Versioning;
using M05.UrlPathVersionionMinimal.Data;
using M05.UrlPathVersionionMinimal.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});

var app = builder.Build();


app.MapProductEndpoints();


app.Run();