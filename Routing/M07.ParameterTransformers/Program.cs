using M07.ParameterTransformers.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
    options.ConstraintMap["slugify"] = typeof(SlugifyTransformer)
);

var app = builder.Build();

app.MapGet("/book/{title:slugify}", (string title) =>
{
    return Results.Ok(new { title });
}).WithName("BookByTitle");



app.MapGet("/generate/{tit}", (string tit,LinkGenerator link, HttpContext context) =>
{
    var url = link.GetPathByName("BookByTitle", new
    {
        title = tit
    });
    return Results.Ok(new { generatedUrl = url });
});

app.Run();
