using Microsoft.AspNetCore.Mvc;

namespace M03.ResultFilters.Controllers;

[ApiController]
[Route("api/document")]
public class DocumentController() : ControllerBase
{

    [HttpGet("{docNo}")]
    public IActionResult Get(int docNo)
    {
        // fake pull filename from docNo
        string fileName = "somefile.pdf";

        var filePath = Path.Combine("C:\\SensitiveFiles", fileName);

        if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);

        return PhysicalFile(filePath, "application/pdf", fileName);
    }
}

