

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



var app = builder.Build();



app.MapControllers();

app.MapGet("/product-minimal", ([FromForm] string name,[FromForm] decimal price) =>
{
    return Results.Ok(new { name, price });
}).DisableAntiforgery();


app.MapPost("/upload-minimal", async(IFormFile file) =>
{
    if (file is null || file.Length == 0)
        return Results.BadRequest("no file uploaded");

    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    Directory.CreateDirectory(uploads);

    var path = Path.Combine(uploads, file.FileName);
    using var stream = new FileStream(path, FileMode.Create);
    await file.CopyToAsync(stream);

    return Results.Ok("uploaded");
}).DisableAntiforgery();

app.Run();
