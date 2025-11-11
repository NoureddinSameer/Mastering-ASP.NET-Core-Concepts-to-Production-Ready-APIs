

using M08.MediaVersioningMinimal.Data;
using M08.MediaVersioningMinimal.Endpoints.V1;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();


var app = builder.Build();


app.MapProductEndpointsV1();

app.Run();