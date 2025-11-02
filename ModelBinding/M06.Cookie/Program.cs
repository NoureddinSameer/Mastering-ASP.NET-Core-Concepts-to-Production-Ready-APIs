


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



var app = builder.Build();

app.MapControllers();

app.MapGet("/mn/preferences", (HttpContext httpContext) =>
{
    var theme = httpContext.Request.Cookies["theme"];
    var language = httpContext.Request.Cookies["language"];
    var timeZone = httpContext.Request.Cookies["timeZone"];
    return Results.Ok(new
    {
        Theme = theme,
        Language = language,
        TimeZone = timeZone
    });
});
app.Run();
