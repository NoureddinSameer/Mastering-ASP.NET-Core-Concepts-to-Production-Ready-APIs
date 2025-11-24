
using M05.EndpointFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("api/products", () =>
{
  return new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" };
});

app.Run();


