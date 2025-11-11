
using M05.UrlPathVersionionMinimal.Data;
using M05.UrlPathVersionionMinimal.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();


var app = builder.Build();


app.MapProductEndpoints();


app.Run();