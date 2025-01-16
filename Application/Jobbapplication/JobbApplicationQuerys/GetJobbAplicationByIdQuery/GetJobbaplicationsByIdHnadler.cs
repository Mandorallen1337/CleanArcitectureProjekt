using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationQuerys.GetJobbAplicationByIdQuery
{
    public class GetJobbApplicationByIdHandler : IRequestHandler<GetJobbApplicationByIdQuery, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public GetJobbApplicationByIdHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(GetJobbApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var jobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (jobbApplication == null)
            {
                throw new KeyNotFoundException("Job application not found.");
            }

            return jobbApplication;
        }
    }
}
