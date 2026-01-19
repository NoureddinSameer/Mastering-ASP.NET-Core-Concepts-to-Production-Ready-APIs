

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using M02.MinimalDataAnnotations.Requests;
using M02.MinimalDataAnnotations.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapPost("/api/products", (CreateProductRequest? request) =>
{
    return Results.Created($"/api/products/{Guid.NewGuid()}", request);
}).Validate<CreateProductRequest>();

app.Run();
