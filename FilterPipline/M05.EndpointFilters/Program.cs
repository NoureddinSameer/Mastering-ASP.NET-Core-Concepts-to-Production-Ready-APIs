using M05.EndpointFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// app.MapGet("api/products", () =>
// {
//   return new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" };
// }).AddEndpointFilter<EnvelopeResultFilter>()   // Endpoint level Filter Registration
//   .AddEndpointFilter<TrackActionTimeEndpointFilter>();  // Endpoint level Filter Registration

var globalGroup = app.MapGroup("")
      .AddEndpointFilter<EnvelopeResultFilter>()   // Global Filter Registration
     .AddEndpointFilter<TrackActionTimeEndpointFilter>();


globalGroup.MapGet("api/products", () =>
{
  return new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" };
});


globalGroup.MapGet("api/customers", () =>
{
  return new[] { "Ahmad [HR]", "Maisa, [Finance]" };
});


app.Run();


