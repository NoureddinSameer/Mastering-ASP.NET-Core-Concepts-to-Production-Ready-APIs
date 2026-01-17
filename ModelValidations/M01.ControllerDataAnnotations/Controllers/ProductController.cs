using M01.BasicSetup.Requests;
using Microsoft.AspNetCore.Mvc;

namespace M01.ControllerDataAnnotations.Controllers;

// [ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{

    [HttpPost]
    public IActionResult Post([FromBody]CreateProductRequest request)
    {
        if(!ModelState.IsValid)
            return ValidationProblem(ModelState);
        return Created($"/api/products/{Guid.NewGuid()}", request);
    }
}