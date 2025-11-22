
using M03.ResultFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // options.Filters.Add<EnvelopeResultFilter>();
});

builder.Services.AddScoped<EnvelopeResultFilter>();

var app = builder.Build();

app.MapControllers();

app.Run();


