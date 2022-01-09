using Microsoft.AspNetCore.Mvc;

namespace Book_store.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet("get-all1")]
        public IActionResult GetAll()
        {
            return Ok("This from V1");
        }
    }
}
