using Application.Dtos;
using Application.Interfaces.RepoInterface;
using Application.Queries.JobbApplicationQuerys;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class GetJobbApplicationByIdHandler : IRequestHandler<GetJobbApplicationByIdQuery, JobbApplicationDto>
{
    private readonly IRepository<JobbApplication> _jobbApplicationRepository;

    public GetJobbApplicationByIdHandler(IRepository<JobbApplication> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<JobbApplicationDto> Handle(GetJobbApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämta jobbansökan från databasen
        var jobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (jobbApplication == null) return null;

        // Returnera en DTO
        return new JobbApplicationDto
        {
            JobTitle = jobbApplication.JobTitle,
            CompanyName = jobbApplication.CompanyName
        };
    }
}
