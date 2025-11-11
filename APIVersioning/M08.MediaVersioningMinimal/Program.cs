

using M08.MediaVersioningMinimal.Data;
using M08.MediaVersioningMinimal.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();


var app = builder.Build();


app.MapProductEndpoints();

app.Run();