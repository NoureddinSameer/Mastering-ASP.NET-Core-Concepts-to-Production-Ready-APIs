
using M08.MediaVersioningMinimal.Data;
using M08.MediaVersioningMinimal.Responses.V1;
using Microsoft.AspNetCore.Http.HttpResults;

namespace M08.MediaVersioningMinimal.Endpoints;

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


        return TypedResults.Ok(ProductResponse.FromModel(product));
    }
}