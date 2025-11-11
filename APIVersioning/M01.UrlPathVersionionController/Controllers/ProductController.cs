using M01.UrlPathVersioningController.Data;
using M01.UrlPathVersioningController.Responses;
using Microsoft.AspNetCore.Mvc;

namespace M01.UrlPathVersioningController.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpGet("{productId}")]
    public ActionResult<ProductResponse> GetProduct(Guid productId)
    {
        var product = repository.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductResponse.FromModel(product));
    }
}