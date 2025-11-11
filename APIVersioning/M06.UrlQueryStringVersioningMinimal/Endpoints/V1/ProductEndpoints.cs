
using M06.UrlQueryStringVersioningMinimal.Data;
using M06.UrlQueryStringVersioningMinimal.Responses.V1;
using Microsoft.AspNetCore.Http.HttpResults;

namespace M06.UrlQueryStringVersioningMinimal.Endpoints.V1;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpointsV1(this IEndpointRouteBuilder app)
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