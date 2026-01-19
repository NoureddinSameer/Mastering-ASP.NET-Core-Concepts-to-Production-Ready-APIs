

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using M02.MinimalDataAnnotations.Requests;
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
}).AddEndpointFilter(async (context,next) =>
{
    var argument = context.Arguments.OfType<CreateProductRequest>().FirstOrDefault();

    if(argument is null)
    {
        return Results.Problem(new ProblemDetails
        {
            Title = "Bad Request",
            Status = StatusCodes.Status400BadRequest,
            Detail = $"{nameof(CreateProductRequest)} is null"
        });
    }
    List<ValidationResult> validationResults = [];

    var isValid = Validator.TryValidateObject(
                            argument,
                            new ValidationContext(argument),
                            validationResults,
                            true);
    if (!isValid)
    {
        var errorGroup = validationResults
            .SelectMany(v=> (v.MemberNames.Any() ? v.MemberNames: new [] { "" })
            .Select(name => new { name, v.ErrorMessage }))
            .GroupBy(x => x.name)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage!).ToArray()
            );
        return Results.ValidationProblem(errorGroup);
    }
    return await next(context);
});

app.Run();
