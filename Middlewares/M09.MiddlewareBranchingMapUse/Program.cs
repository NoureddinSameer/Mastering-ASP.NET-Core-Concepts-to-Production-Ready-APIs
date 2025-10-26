var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here
app.UseWhen(context => context.Request.Path.Equals("/branch1", StringComparison.OrdinalIgnoreCase),
b =>
{
    b.Use(async (context,next) =>
    {
        await context.Response.WriteAsync("MW Branch 1\n");
        await next();
    });
});


app.Run(async context =>
{
    await context.Response.WriteAsync("Terminal middleware Main Pipeline");
});

app.Run();
