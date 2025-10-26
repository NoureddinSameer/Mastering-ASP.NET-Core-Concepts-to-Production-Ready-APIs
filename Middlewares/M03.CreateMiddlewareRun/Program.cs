var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("This is an end  of pipeline (terminal middleware)");
});

// 03 Middleware intercept httpcontext object
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #3 (HttpContext context,RequestDelegate next) \n");
    await next(context);
});

app.Run();
