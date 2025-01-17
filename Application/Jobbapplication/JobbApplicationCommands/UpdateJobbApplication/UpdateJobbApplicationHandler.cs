using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationCommands.UpdateJobbApplication
{
    public class CreateJobbApplicationHandler : IRequestHandler<CreateJobbApplicationCommand, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public CreateJobbApplicationHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(CreateJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            // Map command to entity
            var jobbApplicationEntity = new JobbApplication
            {
                JobTitle = request.JobTitle,
                CompanyName = request.CompanyName
            };

            // Save entity to repository
            var createdEntity = await _jobbApplicationRepository.CreateAsync(jobbApplicationEntity);

            // Return the created entity
            return createdEntity;
        }
    }
}
