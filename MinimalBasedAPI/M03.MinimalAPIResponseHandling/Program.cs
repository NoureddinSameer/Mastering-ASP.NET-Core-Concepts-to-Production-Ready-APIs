
using M03.MinimalAPIResponseHandling.Data;
using M03.MinimalAPIResponseHandling.Models;
using Microsoft.AspNetCore.Http.HttpResults;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapGet("/text", () =>"Hello World");


app.MapGet("/json", () => new { Message = "Hello World" });


// le meaning lambda expression ,ir meaning IResult
app.MapGet("/api/products-le-ir/{id:guid}", (Guid id, ProductRepository repository) =>
{
    var product = repository.GetProductById(id);

    return product is null
            ? Results.NotFound()
            : Results.Ok(product);
});


// mr meaning method reference ,ir meaning IResult
app.MapGet("/api/products-mr-ir/{id:guid}", GetProductIResult);

static IResult GetProductIResult(Guid id, ProductRepository repository)
{
    var product = repository.GetProductById(id);

    return product is null
            ? Results.NotFound()
            : Results.Ok(product);
}

//le meaning lambda expression ,tr meaning TypedResults
app.MapGet("/api/products-le-tr/{id:guid}",
Results<Ok<Product>, NotFound> (Guid id, ProductRepository repository) =>
{
    var product = repository.GetProductById(id);

    return product is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(product);
});

// mr meaning method reference ,tr meaning TypedResults
app.MapGet("/api/products-mr-tr/{id:guid}",GetProductTypedResults);

static Results<Ok<Product>,NotFound> GetProductTypedResults(Guid id, ProductRepository repository)
{
    var product = repository.GetProductById(id);

    return product is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(product);
}

app.Run();
