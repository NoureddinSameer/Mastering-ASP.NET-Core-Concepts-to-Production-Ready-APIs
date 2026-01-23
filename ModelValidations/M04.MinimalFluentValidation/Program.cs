
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using M04.MinimalFluentValidation.Requests;
using M04.MinimalFluentValidation.Validators;
using M04.MinimalFluentValidation.Filters;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

var app = builder.Build();

app.MapPost("/api/products", (CreateProductRequest? request) =>
{
    return Results.Created($"/api/products/{Guid.NewGuid()}", request);
}).AddEndpointFilter<ValidationFilter<CreateProductRequest>>();

app.Run();
