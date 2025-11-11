

using M06.UrlQueryStringVersioningMinimal.Data;
using M06.UrlQueryStringVersioningMinimal.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();



var app = builder.Build();

app.MapProductEndpoints();


app.Run();