var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here

app.MapWhen(context =>
    context.Request.Path.Equals("/checkout", StringComparison.OrdinalIgnoreCase) &&
    context.Request.Query["mode"] == "new",
b =>
{
    b.Run(async context =>
    {
        await context.Response.WriteAsync("Modern checkout processing pipeline");
    });
});
app.Map("/checkout",b=>
{
    b.Run(async context =>
    {
        await context.Response.WriteAsync("Legacy checkout processing pipeline");
    });
});

app.Run();
