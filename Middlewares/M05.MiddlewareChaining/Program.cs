
var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("MW #1 Before\n");
    await next();
    await context.Response.WriteAsync("MW #1 After\n");
});
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("     MW #2 Before\n");
    await next();
    await context.Response.WriteAsync("     MW #2 After\n");
});
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("             MW #3 Before\n");
    await next();
    await context.Response.WriteAsync("             MW #3 After\n");
});
app.Run();
