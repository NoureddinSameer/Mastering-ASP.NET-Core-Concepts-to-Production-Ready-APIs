using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



var app = builder.Build();

app.MapGet("/product-minimal/{id:int}", (int id) =>
{
    return Results.Ok(id);
});

app.MapGet("/product-minimal-1/{id:int}", ([FromRoute(Name = "id")] int identifier) =>
{
    return Results.Ok(identifier);
});



app.MapGet("/product-minimal-2/{id:int}", (int id) =>
{
    return Results.Ok(id);
});
app.MapGet("/product-minimal-3/{id:int}", ([FromQuery]int id) =>
{
    return Results.Ok(id);
});

app.MapControllers();

app.Run();
