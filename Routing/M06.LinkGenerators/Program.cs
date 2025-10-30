var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/order/{id:int}", (int id,LinkGenerator link,HttpContext context) =>
{
    // order is retrieved
    var editUrl = link.GetUriByName(
        "EditOrder",
        new {id},
        context.Request.Scheme,
        context.Request.Host
    );

    return Results.Ok(new
    {
        orderId = id,
        status = "PENDING",
        _links = new
        {
            self = new { href = context.Request.Path },
            edit = new { href = editUrl, Method = "PUT" }
        }
    });
});

app.MapPut("/order/{id:int}", (int id) =>
{
    // order is updated
    return Results.NoContent();
}).WithName("EditOrder");


app.Run();
