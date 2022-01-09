using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Book_store.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;
        public TestsController(ILogger<TestsController> logger)
        {
            _logger = logger;
        }
        [HttpGet("get-all2")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Test log");
            return Ok("This from V2");
        }
    }
}
