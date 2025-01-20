using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommandHandler : IRequestHandler<CreateCVCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public CreateCVCommandHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CV> Handle(CreateCVCommand request, CancellationToken cancellationToken)
        {
            var cvEntity = new CV
            {
                FileUrl = request.NewCV.FileUrl,
                UploadDate = DateTime.UtcNow,
                UserId = request.NewCV.UserId
            };

            var createdEntity = await _cvRepository.CreateAsync(cvEntity);

            return createdEntity;
        }
    }
}
