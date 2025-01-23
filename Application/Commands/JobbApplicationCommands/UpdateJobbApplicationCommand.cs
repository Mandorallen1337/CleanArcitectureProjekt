using MediatR;
using System;

namespace Application.Commands.JobbApplicationCommands
{
    public class UpdateJobbApplicationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public Guid UserId { get; set; }
    }
}
