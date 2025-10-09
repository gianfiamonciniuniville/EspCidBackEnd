using Microsoft.AspNetCore.Mvc;

namespace EspCid.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Test");
    }
}
