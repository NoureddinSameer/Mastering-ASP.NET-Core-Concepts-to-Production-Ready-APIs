

using M07.HeaderVersioningMinimal.Data;
using M07.HeaderVersioningMinimal.Endpoints.V1;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();


app.MapProductEndpointsV1();


app.Run();