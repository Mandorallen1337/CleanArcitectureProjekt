using Application.Dtos;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.JobbApplicationQuerys;

public class GetAllJobbAplicationsHandler : IRequestHandler<GetAllJobbApplicationsQuery, List<JobbApplicationDto>>
{
    private readonly IRepository<JobbApplication> _jobbApplicationRepository;

    public GetAllJobbAplicationsHandler(IRepository<JobbApplication> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<List<JobbApplicationDto>> Handle(GetAllJobbApplicationsQuery request, CancellationToken cancellationToken)
    {
        // Hämta alla jobbansökningar från databasen
        var jobbApplications = await _jobbApplicationRepository.GetAllAsync();

        // Mappar till en lista av DTOs
        var jobbApplicationDtos = jobbApplications.Select(j => new JobbApplicationDto
        {
            JobTitle = j.JobTitle,
            CompanyName = j.CompanyName
        }).ToList();

        return jobbApplicationDtos;
    }
}
