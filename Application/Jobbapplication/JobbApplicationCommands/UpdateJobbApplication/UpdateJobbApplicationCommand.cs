using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationCommands.UpdateJobbApplication
{
    public class CreateJobbApplicationCommand : IRequest<JobbApplication>
    {
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
