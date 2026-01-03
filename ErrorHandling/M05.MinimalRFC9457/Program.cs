
using M05.MinimalRFC9457.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();



app.MapErrorEndpoints();

app.Run();
