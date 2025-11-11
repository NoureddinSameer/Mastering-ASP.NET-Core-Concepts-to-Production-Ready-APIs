

using M06.UrlQueryStringVersioningMinimal.Data;
using M06.UrlQueryStringVersioningMinimal.Endpoints.V1;
using M06.UrlQueryStringVersioningMinimal.Endpoints.V2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();



var app = builder.Build();

app.MapProductEndpointsV1();
// app.MapProductEndpointsV2();

app.Run();