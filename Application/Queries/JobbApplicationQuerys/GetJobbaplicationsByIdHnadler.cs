
using Application.Interfaces.RepoInterface;
using Application.Queries.JobbApplicationQuerys;
using Domain.Models;
using Domain.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class GetJobbApplicationByIdHandler : IRequestHandler<GetJobbApplicationByIdQuery, JobApplicationViewModel>
{
    private readonly IRepository<JobbApplicationViewModel> _jobbApplicationRepository;

    public GetJobbApplicationByIdHandler(IRepository<JobbApplicationViewModel> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<JobApplicationViewModel> Handle(GetJobbApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämta jobbansökan från databasen
        var jobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (jobbApplication == null) return null;

        // Returnera en DTO
        return new JobApplicationViewModel
        {
            JobTitle = jobbApplication.JobTitle,
            CompanyName = jobbApplication.CompanyName
        };
    }
}
