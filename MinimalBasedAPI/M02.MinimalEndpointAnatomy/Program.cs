
using M02.MinimalEndpointAnatomy.Data;
using M02.MinimalEndpointAnatomy.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();




// app.MapGet("/api/products", (ProductRepository repository) =>
// {
//     // Handles retrieving a list of products
//     return Results.Ok(repository.GetProductsPage());
// });
app.MapGet("/api/products", GetProducts);



// app.MapGet("/api/products/{id:guid}", (Guid id,ProductRepository repository) =>
// {
//     // Handles retrieving a single product by its unique identifier
//     var product = repository.GetProductById(id);
//     return product is null ? Results.NotFound():Results.Ok(ProductResponse.FromModel(product));
// });
app.MapGet("/api/products/{id:guid}", GetProduct);



// IResult GetProducts(ProductRepository repository)
// {
//     // Handles retrieving a list of products
//     return Results.Ok(repository.GetProductsPage());
// }
async Task<IResult> GetProducts(ProductRepository repository)
{
    await Task.Delay(1000);
    // Handles retrieving a list of products
    return Results.Ok(repository.GetProductsPage());
}



IResult GetProduct(Guid id,ProductRepository repository)
{
    // Handles retrieving a single product by its unique identifier
    var product = repository.GetProductById(id);
    return product is null ? Results.NotFound():Results.Ok(ProductResponse.FromModel(product));
}
app.Run();
