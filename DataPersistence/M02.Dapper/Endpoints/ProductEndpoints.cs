using M02.Dapper.Models;
using M02.Dapper.Requests;
using M02.Dapper.Responses;
using Microsoft.AspNetCore.Http.HttpResults;


namespace M02.Dapper.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var productApi = app.MapGroup("/api/products");

        productApi.MapGet("", GetPaged);
        productApi.MapGet("{productId:guid}", GetProductById).WithName(nameof(GetProductById));
        productApi.MapPost("", CreateProduct);
        productApi.MapPost("{productId:guid}/reviews", CreateProductReview);
        productApi.MapPut("{productId:guid}", Put);
        productApi.MapDelete("{productId:guid}", Delete);

        return productApi;
    }

    private static IResult GetPaged(
        ProductRepository repository,
        int page = 1,
        int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        int totalCount = repository.GetProductsCount();

        var products = repository.GetProductsPage(page, pageSize);

        var pagedResult = PagedResult<ProductResponse>.Create(
            ProductResponse.FromModels(products),
            totalCount,
            page,
            pageSize
        );

        return Results.Ok(pagedResult);
    }

    private static Results<Ok<ProductResponse>, NotFound<string>> GetProductById(
        Guid productId,
        ProductRepository repository,
        bool includeReviews = false)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return TypedResults.NotFound($"Product with id '{productId}' not found");

        List<ProductReview>? reviews = null;

        if (includeReviews == true)
            reviews = repository.GetProductReviews(productId);

        return TypedResults.Ok(ProductResponse.FromModel(product, reviews));
    }

    private static IResult CreateProduct(CreateProductRequest request, ProductRepository repository)
    {
         if (repository.ExistsByName(request.Name))
            return Results.Conflict($"A product with the name '{request.Name}' already exists.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        repository.AddProduct(product);

        return Results.CreatedAtRoute(routeName: nameof(GetProductById),
                              routeValues: new { productId = product.Id },
                              value: ProductResponse.FromModel(product));
    }

    private static IResult CreateProductReview(
        Guid productId,
        CreateProductReviewRequest request,
        ProductRepository repository)
    {
        if(!repository.ExistsById(productId))
            return Results.NotFound($"Product with id '{productId}' not found");

        var productReview = new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Reviewer = request.Reviewer,
            Stars = request.Stars
        };

        repository.AddProductReview(productReview);

        return Results.Created(
            $"/api/products/{productId}/reviews/{productReview.Id}",
            ProductReviewResponse.FromModel(productReview)
        );
    }

    private static IResult Put(Guid productId, UpdateProductRequest request, ProductRepository repository)
    {
        var product = repository.GetProductById(productId);
        if (product is null)
            return Results.NotFound($"Product with id '{productId}' not found");

        product.Name = request.Name;
        product.Price = request.Price ?? 0;

        var success = repository.UpdateProduct(product);

        if (!success)
            return Results.StatusCode(500);

        return Results.NoContent();
    }

    private static IResult Delete(Guid productId, ProductRepository repository)
    {
        if (!repository.ExistsById(productId))
            return Results.NotFound($"Product with id '{productId}' not found");

        var succeeded = repository.DeleteProduct(productId);

        if (!succeeded)
            return Results.StatusCode(500);

        return Results.NoContent();
    }
}