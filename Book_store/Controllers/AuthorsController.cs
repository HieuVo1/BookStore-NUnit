using Book_store.DTOs.Authors.Requests;
using Book_store.DTOs.Commons;
using Book_store.Services.Authors;
using Book_store.Services.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService, ILoggerManager logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(AuthorCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid class object sent from client.");
                return BadRequest();
            }

            var result = await _authorService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll([FromQuery] QueryStringParameter parameters)
        {
            var result = _authorService.GetAll(parameters);
            return Ok(result);
        }

        [HttpGet("get-by-id/{authorId}")]
        public IActionResult GetByID(int authorId)
        {
            var result = _authorService.GetByID(authorId);
            return Ok(result);
        }

        [HttpGet("get-by-id-with-books/{authorId}")]
        public IActionResult GetByIDWithBooks(int authorId)
        {
            var result = _authorService.GetByIDWithBooks(authorId);
            return Ok(result);
        }

        [HttpDelete("delete/{authorId}")]
        public async Task<IActionResult> DeleteAsync(int authorId)
        {
            var result = await _authorService.DeleteAsync(authorId);
            return Ok(result);
        }

        [HttpPatch("update/{authorId}")]
        public async Task<IActionResult> UpdateAsync(int authorId, AuthorCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book object sent from client.");
                return BadRequest();
            }

            var result = await _authorService.UpdateAsync(authorId, request);
            return Ok(result);
        }
    }
}
