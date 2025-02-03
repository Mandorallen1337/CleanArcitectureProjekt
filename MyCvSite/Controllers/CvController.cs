using Application.Commands.CVCommands;
using Application.Queries.CVQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCvSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CVController> _logger;


        public CVController(IMediator mediator, ILogger<CVController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IFormFile cv, [FromForm] Guid userId)
        {
            try
            {
                var createCvResult = await _mediator.Send(new CreateCVCommand(cv, userId));
                _logger.LogInformation("Successfully created a new CV with ID {CvId}.", createCvResult.Id);
                return Ok(createCvResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new CV.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCvs()
        {
            try
            {
                var getAllCvsResult = await _mediator.Send(new GetAllCVsQuery());
                _logger.LogInformation("Successfully retrieved {Count} CVs.", getAllCvsResult.Count());
                return Ok(getAllCvsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving CVs.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCvById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var getCvByIdResult = await _mediator.Send(new GetCVByIdQuery(id));
                if (getCvByIdResult == null)
                {
                    _logger.LogWarning("CV with ID {CvId} was not found.", id);
                    return NotFound($"CV with ID {id} was not found.");
                }
                _logger.LogInformation("Successfully retrieved CV with ID {CvId}.", id);
                return Ok(getCvByIdResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving CV with ID {CvId}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadCVById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var cvResult = await _mediator.Send(new DownloadCVByIdCommand(id));

                if (cvResult == null)
                {
                    _logger.LogWarning("CV with ID {CvId} was not found.", id);
                    return NotFound($"CV with ID {id} not found.");
                }

                return File(cvResult.Content, "application/pdf", cvResult.FileUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCvById(Guid id, [FromForm] IFormFile cv, [FromForm] string userId, CancellationToken cancellationToken)
        {
            try
            {
                var updateCvByIdResult = await _mediator.Send(new UpdateCVByIdCommand(id, cv, userId));
                _logger.LogInformation("Successfully updated CV with ID {CvId}.", id);
                return Ok(updateCvByIdResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating CV with ID {CvId}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCvById(Guid id)
        {
            try
            {
                var deleteCvByIdResult = await _mediator.Send(new DeleteCVByIdCommand(id));
                _logger.LogInformation("Successfully deleted CV with ID {CvId}.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting CV with ID {CvId}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("analyze/{cvId}")]
        public async Task<IActionResult> AnalyzeCv(Guid cvId)
        {
            try
            {
                var result = await _mediator.Send(new AnalyzeCvCommand(cvId));

                if (result == null)
                {
                    _logger.LogWarning("Failed to analyze CV with ID {CvId}.", cvId);
                    return BadRequest("Failed to analyze CV.");
                }

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing CV with ID {CvId}.", cvId);
                return StatusCode(500, "An error occurred while analyzing the CV.");
            }
        }

    }
}
