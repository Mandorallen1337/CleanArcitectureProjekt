using Application.Commands.CVCommands;
using Application.Queries.CVQueries;
using Domain.Models;
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
        public async Task<IActionResult> Create([FromBody] CV cv)
        {
            try
            {
                var createCvResult = await _mediator.Send(new CreateCVCommand(cv));
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCvById(Guid id, [FromBody] CV cv, CancellationToken cancellationToken)
        {
            try
            {
                var updateCvByIdResult = await _mediator.Send(new UpdateCVByIdCommand(id, cv));
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
                return Ok(deleteCvByIdResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting CV with ID {CvId}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
