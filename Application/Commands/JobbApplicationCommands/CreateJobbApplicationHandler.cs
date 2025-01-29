using Application.Commands.JobbApplicationCommands;
using Application.Dtos;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CreateCvCommandHandler : IRequestHandler<CreateJobbApplicationCommand, JobbApplicationViewModel>
{
    private readonly IRepository<JobbApplicationViewModel> _jobbApplicationRepository;

    public CreateCvCommandHandler(IRepository<JobbApplicationViewModel> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<JobbApplicationViewModel> Handle(CreateJobbApplicationCommand request, CancellationToken cancellationToken)
    {
        // Skapa en ny jobbansökan
        var jobbApplication = new JobbApplicationViewModel
        {
            JobTitle = request.JobTitle,
            CompanyName = request.CompanyName,
            ApplicationDate = DateTime.UtcNow,
            Status = false
            
        };

        // Spara jobbansökan i databasen
        var createdJobbApplication = await _jobbApplicationRepository.CreateAsync(jobbApplication);

        return createdJobbApplication;
    }
}
