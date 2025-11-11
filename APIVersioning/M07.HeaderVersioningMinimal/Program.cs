

using M07.HeaderVersioningMinimal.Data;
using M07.HeaderVersioningMinimal.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();


app.MapProductEndpoints();


app.Run();