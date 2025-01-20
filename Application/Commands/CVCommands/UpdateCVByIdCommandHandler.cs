using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommandHandler : IRequestHandler<UpdateCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public UpdateCVByIdCommandHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CV> Handle(UpdateCVByIdCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"CV with ID {request.Id} was not found.");
            }

            existingEntity.FileUrl = request.UpdatedCV.FileUrl;
            existingEntity.UploadDate = DateTime.UtcNow;
            existingEntity.UserId = request.UpdatedCV.UserId;

            await _cvRepository.UpdateAsync(existingEntity, cancellationToken);

            return existingEntity;
        }
    }
}