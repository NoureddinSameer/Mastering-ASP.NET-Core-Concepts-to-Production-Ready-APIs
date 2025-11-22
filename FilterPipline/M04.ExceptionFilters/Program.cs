
// using M03.ResultFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // options.Filters.Add<GlobalExceptionFilter>();
});

var app = builder.Build();

app.MapControllers();

app.Run();


