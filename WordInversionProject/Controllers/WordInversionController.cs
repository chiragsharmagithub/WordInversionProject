using Microsoft.AspNetCore.Mvc;
using WordInversionProject.DTOs;
using WordInversionProject.Interfaces;

namespace WordInversionProject.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	[Produces("application/json")]
	public class WordInversionController : Controller
	{
		private readonly IWordInversionService _service;
		private readonly ILogger<WordInversionController> _logger;
		public WordInversionController(
			IWordInversionService service, 
			ILogger<WordInversionController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpPost("invert")]
		[ProducesResponseType(typeof(WordInversionResponseDto), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<WordInversionResponseDto>> InvertStatement([FromBody] WordInversionRequestDto request)
		{
			try
			{
				var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
				var result = await _service.InvertSentenceAsync(request.Sentence, ipAddress);

				return CreatedAtAction(nameof(GetAllRecords), new {id = result.Id}, result);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error inverting sentence");
				return StatusCode(500, new { error = "Internal server error" });
			}
		}

		[HttpGet("records")]
		[ProducesResponseType(typeof(IEnumerable<WordInversionResponseDto>), 200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<IEnumerable<WordInversionResponseDto>>> GetAllRecords()
		{
			try
			{
				var records = await _service.GetAllRecordsAsync();
				return Ok(records);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all records");
				return StatusCode(500, new { error = "Internal server error" });
			}
		}

		[HttpPost("search")]
		[ProducesResponseType(typeof(IEnumerable<WordInversionResponseDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<WordInversionResponseDto>>> SearchByWord([FromBody] SearchRequestDto request)
		{
			try
			{
				var records = await _service.SearchByWordAsync(request.Word);
				return Ok(records);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error searching records");
				return StatusCode(500, new { error = "Internal server error" });
			}
		}
	}
}
