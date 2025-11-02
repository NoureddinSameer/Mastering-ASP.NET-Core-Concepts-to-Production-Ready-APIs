
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



var app = builder.Build();


app.MapGet("/product-minimal", ([FromHeader(Name = "X-Api-version")] string apiVersion) =>
{
    return Results.Ok($"Api version: {apiVersion}");
});

// we can use this without Name="" but the name of parameter must be the same key value in request so the request will be like this:
// GET {{baseUrl}}/product-minimal
// apiVersion: v1

// app.MapGet("/product-minimal", ([FromHeader]string apiVersion) =>
// {
//     return Results.Ok($"Api version: {apiVersion}");
// });

app.MapControllers();

app.Run();
