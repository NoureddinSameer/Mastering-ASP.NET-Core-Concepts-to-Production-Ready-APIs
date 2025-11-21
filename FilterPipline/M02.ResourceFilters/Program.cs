using M02.ResourceFilters.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<TenantValidationFilter>();

var app = builder.Build();

app.MapControllers();

app.Run();


