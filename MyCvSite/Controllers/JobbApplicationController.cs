using Application.Commands.JobbApplicationCommands;
using Application.Queries.JobbApplicationQuerys;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyCvSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobbApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<JobbApplicationController> _logger;

        public JobbApplicationController(IMediator mediator, ILogger<JobbApplicationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<JobbApplicationViewModel>> GetAllJobbAplications()
        {
            _logger.LogInformation("Fetching all job applications.");
            var jobbApplications = await _mediator.Send(new GetAllJobbApplicationsQuery());
            var jobApplicationViewModels = jobbApplications.Select(j => new JobbApplicationViewModel
            {                
                JobTitle = j.JobTitle,
                CompanyName = j.CompanyName,
                ApplicationDate = j.ApplicationDate,
                Status = j.Status
            }).ToList();
            _logger.LogInformation("Fetched {Count} job applications.", jobbApplications.Count);

            return jobApplicationViewModels;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetJobbaplicationsById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching job application with ID {Id}.", id);
            var jobbApplication = await _mediator.Send(new GetJobbApplicationByIdQuery(id), cancellationToken);
            if (jobbApplication == null)
            {
                _logger.LogWarning("Job application with ID {Id} not found.", id);
                return NotFound("Job application not found.");
            }

            _logger.LogInformation("Fetched job application with ID {Id}.", id);
            return Ok(jobbApplication);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobbApplication([FromBody] JobbApplicationViewModel jobbApplication)
        {
            _logger.LogInformation("Creating a new job application for job title {JobTitle}.", jobbApplication.JobTitle);

            // Skapa ett nytt CreateJobbApplicationCommand och skicka till MediatR
            var createCommand = new CreateJobbApplicationCommand
            {
                JobTitle = jobbApplication.JobTitle,
                CompanyName = jobbApplication.CompanyName,
                ApplicationDate = jobbApplication.ApplicationDate,
                Status = jobbApplication.Status
            };

            await _mediator.Send(createCommand);

            // Hämta den uppdaterade listan efter skapandet
            var updatedJobApplications = await _mediator.Send(new GetAllJobbApplicationsQuery());

            _logger.LogInformation("Returning updated job applications list.");
            return Ok(updatedJobApplications);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateJobbApplication(Guid id, [FromBody] UpdateJobbApplicationCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("Job application ID mismatch.");
            }

            _logger.LogInformation("Updating job application with ID {Id}.", id);

            // Skicka kommandot via MediatR
            await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Updated job application with ID {Id}.", id);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting job application with ID {Id}.", id);
            var result = await _mediator.Send(new DeleteJobbApplicationCommand(id));
            if (!result)
            {
                _logger.LogWarning("Job application with ID {Id} not found.", id);
                return NotFound("Entity not found");
            }

            _logger.LogInformation("Deleted job application with ID {Id}.", id);
            return Ok($"Job application with ID {id} was deleted.");
        }
    }   
}
