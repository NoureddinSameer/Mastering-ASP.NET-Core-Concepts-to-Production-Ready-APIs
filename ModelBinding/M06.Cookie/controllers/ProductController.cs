
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("preferences")]
public class ProductController : ControllerBase
{

    [HttpGet]
    public IActionResult GetPreferences()
    {
        var theme = HttpContext.Request.Cookies["theme"];
        var language = HttpContext.Request.Cookies["language"];
        var timeZone = HttpContext.Request.Cookies["timeZone"];
        return Ok(new
        {
            Theme = theme,
            Language = language,
            TimeZone = timeZone
        });
    }
}