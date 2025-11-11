
using M07.HeaderVersioningMinimal.Data;
using M07.HeaderVersioningMinimal.Responses.V1;
using Microsoft.AspNetCore.Http.HttpResults;

namespace M07.HeaderVersioningMinimal.Endpoints;

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
        ProductRepository repository, HttpResponse response)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return TypedResults.NotFound($"Product with Id '{productId}' not found");

        response.Headers["Deprecation"] = "true";
        response.Headers["Sunset"] = "Wed, 31 Dec 2025 23:59:59 GMT";
        response.Headers["Link"] = "</api/v2/products>; rel=\"successor-version\"";

        return TypedResults.Ok(ProductResponse.FromModel(product));
    }
}