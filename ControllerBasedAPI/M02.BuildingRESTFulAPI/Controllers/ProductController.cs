using System.Text;
using M02.BuildingRESTFulAPI.Data;
using M02.BuildingRESTFulAPI.Models;
using M02.BuildingRESTFulAPI.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace M02.BuildingRESTFulAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpOptions]
    public IActionResult OptionsProducts()
    {
        Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, PATCH, DELETE, OPTIONS");
        return NoContent();
    }
    [HttpHead("{ProductId:guid}")]
    public IActionResult HeadProduct(Guid ProductId)
    {
        return repository.ExistsById(ProductId) ? Ok() : NotFound();
    }
    [HttpGet]
    public IActionResult GetPaged(int page = 1, int pageSize = 10)
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

        return Ok(pagedResult);
    }
    [HttpGet("{productId:guid}", Name = "GetProductById")]
    public ActionResult<ProductResponse> GetProductById(Guid productId, bool includeReviews = false)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return NotFound();

        List<ProductReview>? reviews = null;

        if (includeReviews == true)
            reviews = repository.GetProductReviews(productId);

        return ProductResponse.FromModel(product, reviews);
    }
    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        if (repository.ExistsByName(request.Name))
            return Conflict($"A product with the name '{request.Name}' already exists.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        repository.AddProduct(product);

        return CreatedAtRoute(routeName: nameof(GetProductById),
                              routeValues: new { productId = product.Id },
                              value: ProductResponse.FromModel(product));
    }
    [HttpPut("{ProductId:guid}")]
    public IActionResult Put(Guid productId, UpdateProductRequest request)
    {
        var product = repository.GetProductById(productId);
        if (product is null)
            return NotFound($"Product with id '{productId}' not found");

        product.Name = request.Name;
        product.Price = request.Price ?? 0;

        var success = repository.UpdateProduct(product);

        if (!success)
            return StatusCode(500, "Failed to update product");

        return NoContent();
    }

    [HttpPatch("{ProductId:guid}")]
    public IActionResult Patch(Guid productId, JsonPatchDocument<UpdateProductRequest>? patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("Invalid patch document.");

        var product = repository.GetProductById(productId);

        if (product is null)
            return NotFound($"Product with id '{productId}' not found.");
        var udpateModel = new UpdateProductRequest
        {
            Name = product.Name,
            Price = product.Price
        };
        patchDoc.ApplyTo(udpateModel);

        product.Name = udpateModel.Name;
        product.Price = udpateModel.Price ?? 0;

        var succeeded = repository.UpdateProduct(product);

        if (!succeeded)
            return StatusCode(500, "Failed to patch product");
        return NoContent();
    }

    [HttpDelete("{ProductId:guid}")]
    public IActionResult Delete(Guid productId)
    {
        if (!repository.ExistsById(productId))
            return NotFound($"Product with id '{productId}' not found");

        var succeeded = repository.DeleteProduct(productId);

        if (!succeeded)
            return StatusCode(500, "Failed to delete product");

        return NoContent();
    }
    [HttpPost("process")]
    public IActionResult ProcessAsync()
    {
        var jobId = Guid.NewGuid();

        return Accepted(
            $"/api/products/status/{jobId}",
            new { jobId, status = "Processing" }
        );
    }
    [HttpGet("status/{jobId}")]
    public IActionResult GetProcessingStatus(Guid jobId)
    {
        var isStillProcessing = false; // fake it
        return Ok(new { jobId, status = isStillProcessing ? "Processing" : "Completed" });

    }
    [HttpGet("csv")]
    public IActionResult GetProductsCsv()
    {
        var products = repository.GetProductsPage(1, 100);
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Id,Name,Price");
        foreach (var product in products)
        {
            csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Price}");
        }
        var fileBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        return File(fileBytes, "text/csv", "product-catalog_1_100.csv");
    }
    [HttpGet("Physical-csv-file")]
    public IActionResult GetPhysicalFile()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");
        return PhysicalFile(filePath, "text/csv", "products-export.csv");

    }
    [HttpGet("products-legacy")]
    public IActionResult GetRedirect()
    {
        return Redirect("/api/products/temp-products");
    }
    [HttpGet("temp-products")]
    public IActionResult TempProducts()
    {
        return Ok(new { message = "You're in the temp endpoint. Chill." });
    }

    [HttpGet("legacy-products")]
    public IActionResult GetPermanentRedirect()
    {
        return RedirectPermanent("/api/products/product-catalog");
    }
    [HttpGet("product-catalog")]
    public IActionResult Catalog()
    {
        return Ok(new { message = "This is the permanent new location." });
    }
}
