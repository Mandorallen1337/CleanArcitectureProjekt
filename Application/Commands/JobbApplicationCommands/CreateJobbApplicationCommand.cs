using Application.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobbApplicationCommands
{
    public class CreateJobbApplicationCommand : IRequest<JobbApplicationViewModel>
    {
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = false;
        public Guid UserId { get; set; }
    }

}
