
using M02.DeveloperExceptionPage.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
else
{
    app.UseExceptionHandler("/error-development");
}
app.MapControllers();

app.MapErrorEndpoints();

app.Run();
