using Book_store.DTOs.Commons;
using Book_store.DTOs.Publishers.Requests;
using Book_store.Services.Loggers;
using Book_store.Services.Publishers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService, ILoggerManager logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(PublisherCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid class object sent from client.");
                return BadRequest();
            }

            var result = await _publisherService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll([FromQuery] QueryStringParameter parameters)
        {
            var result = _publisherService.GetAll(parameters);
            return Ok(result);
        }

        [HttpGet("get-by-id/{publisherId}")]
        public IActionResult GetByID(int publisherId)
        {
            var result = _publisherService.GetByID(publisherId);
            return Ok(result);
        }

        [HttpGet("get-by-id-with-books/{publisherId}")]
        public IActionResult GetByIDWithBooks(int publisherId)
        {
            var result = _publisherService.GetByIDWithBooks(publisherId);
            return Ok(result);
        }

        [HttpDelete("delete/{publisherId}")]
        public async Task<IActionResult> DeleteAsync(int publisherId)
        {
            var result = await _publisherService.DeleteAsync(publisherId);
            return Ok(result);
        }

        [HttpPatch("update/{publisherId}")]
        public async Task<IActionResult> UpdateAsync(int publisherId, PublisherCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book object sent from client.");
                return BadRequest();
            }

            var result = await _publisherService.UpdateAsync(publisherId, request);
            return Ok(result);
        }
    }
}
