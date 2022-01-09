using Book_store.DTOs.Books.Requests;
using Book_store.DTOs.Commons;
using Book_store.Services.Books;
using Book_store.Services.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService, ILoggerManager logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(BookCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid class object sent from client.");
                return BadRequest();
            }

            var result = await _bookService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll([FromQuery] QueryStringParameter parameters)
        {
            var result = _bookService.GetAll(parameters);
            return Ok(result);
        }

        [HttpGet("get-by-id/{bookId}")]
        public IActionResult GetByID(int bookId)
        {
            var result = _bookService.GetByID(bookId);
            return Ok(result);
        }

        [HttpGet("get-by-id-with-authors/{bookId}")]
        public IActionResult GetByIDWithAuthors(int bookId)
        {
            var result = _bookService.GetByIDWithAuthors(bookId);
            return Ok(result);
        }

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteAsync(int bookId)
        {
            var result = await _bookService.DeleteAsync(bookId);
            return Ok(result);
        }

        [HttpPatch("update/{bookId}")]
        public async Task<IActionResult> UpdateAsync(int bookId, BookCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book object sent from client.");
                return BadRequest();
            }

            var result = await _bookService.UpdateAsync(bookId, request);
            return Ok(result);
        }

    }
}
