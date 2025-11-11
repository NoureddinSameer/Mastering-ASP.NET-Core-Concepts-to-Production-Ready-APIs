using Microsoft.AspNetCore.Http.HttpResults;
using M05.UrlPathVersionionMinimal.Data;
using M05.UrlPathVersionionMinimal.Responses;


namespace M05.UrlPathVersionionMinimal.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var productApi = app.MapGroup("api/products");

        productApi.MapGet("{productId:guid}", GetProductById).WithName(nameof(GetProductById));

        return productApi;
    }

    private static Results<Ok<ProductResponse>, NotFound<string>> GetProductById(
        Guid productId,
        ProductRepository repository)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return TypedResults.NotFound($"Product with Id '{productId}' not found");

        return TypedResults.Ok(ProductResponse.FromModel(product));
    }
}