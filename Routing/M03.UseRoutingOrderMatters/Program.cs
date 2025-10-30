var builder = WebApplication.CreateBuilder(args);




var app = builder.Build();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint()?.DisplayName ?? "No Endpoint defined!!";
    Console.WriteLine($"MW #1 {endpoint}");
    await next();
});

app.UseRouting();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint()?.DisplayName ?? "No Endpoint defined!!";
    Console.WriteLine($"MW #2 {endpoint}");
    await next();
});


app.MapGet("/products", () =>
{
    return Results.Ok(new[]
    {
        "Product #1",
        "Product #2"
    });
});
app.Run();
