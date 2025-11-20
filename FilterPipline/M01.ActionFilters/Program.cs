using M01.ActionFilters.Filters;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<SampleActionFilter>(); // Global Filter Registrations
});

var app = builder.Build();

app.MapControllers();

app.Run();


