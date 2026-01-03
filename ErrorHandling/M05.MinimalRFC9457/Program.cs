
using M05.MinimalRFC9457.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapErrorEndpoints();

app.Run();
