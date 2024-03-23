using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AngularApp2.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController(IOptions<CustomSettings> options) : ControllerBase
{
    [HttpGet]
    public async Task<CustomSettings> GetCustomSettings()
    {
        return options.Value;
    }
}
