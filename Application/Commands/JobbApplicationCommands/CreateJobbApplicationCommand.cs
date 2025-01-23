using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobbApplicationCommands
{
    public class CreateJobbApplicationCommand : IRequest<JobbApplicationDto>
    {
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }

}
