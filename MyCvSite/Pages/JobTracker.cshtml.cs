using Application.Commands.JobbApplicationCommands;
using Application.Dtos;
using Application.Queries.JobbApplicationQuerys;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvSite.Controllers;


namespace MyCvSite.Pages
{
    public class JobTrackerModel : PageModel
    {

        private readonly JobbApplicationController _jobbApplicationController;
        public List<JobbApplicationViewModel> JobApplications { get; set; } = new();
        [BindProperty]
        public JobbApplicationViewModel NewJobApplication { get; set; } = new();

        public JobTrackerModel(JobbApplicationController jobbApplicationController)
        {
            _jobbApplicationController = jobbApplicationController;


        }
        public async Task OnGet()
        {
            JobApplications = await _jobbApplicationController.GetAllJobbAplications();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var newApplication = new JobbApplicationViewModel
                {
                    JobTitle = NewJobApplication.JobTitle,
                    CompanyName = NewJobApplication.CompanyName,
                    ApplicationDate = NewJobApplication.ApplicationDate,
                    Status = NewJobApplication.Status
                };

                var response = await _jobbApplicationController.CreateJobbApplication(newApplication);

                if (response is OkObjectResult result && result.Value is List<JobbApplicationViewModel> updatedJobApplications)
                {
                    JobApplications = updatedJobApplications;
                }
                return Page();
            }
            return Page();
        }
    }
}
