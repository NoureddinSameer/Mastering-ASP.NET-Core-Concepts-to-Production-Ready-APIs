var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here

// 01 Middleware do nothing
app.Use((RequestDelegate next) =>next);

// 02 Middleware intercept httpcontext object
app.Use((RequestDelegate next) =>
{
    return async (HttpContext context) =>
    {
        await context.Response.WriteAsync("MW #2 (RequestDelegate next) \n");
        await next(context);
    };
});

// 03 Middleware intercept httpcontext object
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #3 (HttpContext context,RequestDelegate next) \n");
    await next(context);
});

// 04 Middleware intercept httpcontext object
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #4 (HttpContext context,RequestDelegate next) \n");    
});

// 05 Middleware intercept httpcontext object
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #5 (HttpContext context,RequestDelegate next) \n");
    await next(context);
});

app.Run();
