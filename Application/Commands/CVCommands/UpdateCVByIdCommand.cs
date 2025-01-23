using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommand : IRequest<CV>
    {
        public UpdateCVByIdCommand(Guid id, IFormFile updatedCV, string userId)
        {
            Id = id;
            UpdatedCV = updatedCV;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public IFormFile UpdatedCV { get; set; }
        public string UserId { get; set; }
    }
}
