

using System.Text.Json.Serialization;
using M02.MinimalDataAnnotations.Requests;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapPost("/api/products", (CreateProductRequest? request) =>
{
    return Results.Created($"/api/products/{Guid.NewGuid()}", request);
});

app.Run();
