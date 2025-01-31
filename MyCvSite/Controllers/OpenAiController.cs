using Application.Interfaces.OpenAiInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyCvSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;

        public OpenAiController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }



        [HttpGet("analyze")]
        public async Task<IActionResult> AnalyzeDummyText()
        {
            try
            {
                string dummyText = "This is a dummy text for testing purposes.";
                var result = await _openAiService.AnalyzeTextAsync(dummyText);

                if (result == null)
                {
                    return BadRequest("Analysis failed, result was null.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
