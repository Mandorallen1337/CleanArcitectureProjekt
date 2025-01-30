using Application.Commands.JobbApplicationCommands;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public class JobbApplicationTest
    {
        private Mock<IRepository<JobbApplicationViewModel>> _jobbApplicationRepositoryMock;
        private CreateJobbApplicationHandler _createHandler;
        private DeleteJobbApplicationHandler _deleteHandler;
        private UpdateJobbApplicationHandler _updateHandler;

        [SetUp]
        public void SetUp()
        {
            _jobbApplicationRepositoryMock = new Mock<IRepository<JobbApplicationViewModel>>();

            // Initiera handlers
            _createHandler = new CreateJobbApplicationHandler(_jobbApplicationRepositoryMock.Object);
            _deleteHandler = new DeleteJobbApplicationHandler(_jobbApplicationRepositoryMock.Object);
            _updateHandler = new UpdateJobbApplicationHandler(_jobbApplicationRepositoryMock.Object);
        }

        [Test]
        public async Task CreateHandler_ShouldCreateJobbApplicationAndReturnDto()
        {
            // Arrange
            var command = new CreateJobbApplicationCommand
            {
                JobTitle = "Software Engineer",
                CompanyName = "Tech Corp"
            };

            var jobbApplicationEntity = new JobbApplicationViewModel
            {
                Id = Guid.NewGuid(),
                JobTitle = command.JobTitle,
                CompanyName = command.CompanyName,
                ApplicationDate = DateTime.UtcNow,
                Status = false,
                UserId = Guid.NewGuid()
            };

            _jobbApplicationRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<JobbApplicationViewModel>()))
                .ReturnsAsync(jobbApplicationEntity);

            // Act
            var result = await _createHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.JobTitle, Is.EqualTo(command.JobTitle));
            Assert.That(result.CompanyName, Is.EqualTo(command.CompanyName));
            _jobbApplicationRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<JobbApplicationViewModel>()), Times.Once);
        }

        [Test]
        public async Task DeleteHandler_ShouldReturnTrue_WhenEntityIsDeletedSuccessfully()
        {
            // Arrange
            var command = new DeleteJobbApplicationCommand(Guid.NewGuid());

            _jobbApplicationRepositoryMock
                .Setup(repo => repo.DeleteByIdAsync(command.Id))
                .ReturnsAsync("Entity deleted");

            // Act
            var result = await _deleteHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            _jobbApplicationRepositoryMock.Verify(repo => repo.DeleteByIdAsync(command.Id), Times.Once);
        }

        [Test]
        public async Task DeleteHandler_ShouldReturnFalse_WhenEntityNotFound()
        {
            // Arrange
            var command = new DeleteJobbApplicationCommand (Guid.NewGuid());

            _jobbApplicationRepositoryMock
                .Setup(repo => repo.DeleteByIdAsync(command.Id))
                .ReturnsAsync("Entity not found");

            // Act
            var result = await _deleteHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _jobbApplicationRepositoryMock.Verify(repo => repo.DeleteByIdAsync(command.Id), Times.Once);
        }

        [Test]
        public async Task UpdateHandler_ShouldUpdateJobbApplicationSuccessfully()
        {
            // Arrange
            var command = new UpdateJobbApplicationCommand
            {
                Id = Guid.NewGuid(),
                JobTitle = "Updated Title",
                CompanyName = "Updated Company",
                Status = true,
                ApplicationDate = DateTime.UtcNow
            };

            var existingJobbApplication = new JobbApplicationViewModel
            {
                Id = command.Id,
                JobTitle = "Old Title",
                CompanyName = "Old Company",
                Status = false,
                ApplicationDate = DateTime.UtcNow.AddDays(-10)
            };

            _jobbApplicationRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingJobbApplication);

            // Act
            await _updateHandler.Handle(command, CancellationToken.None);

            // Assert
            _jobbApplicationRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            _jobbApplicationRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<JobbApplicationViewModel>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void UpdateHandler_ShouldThrowKeyNotFoundException_WhenEntityNotFound()
        {
            // Arrange
            var command = new UpdateJobbApplicationCommand { Id = Guid.NewGuid() };

            _jobbApplicationRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((JobbApplicationViewModel)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _updateHandler.Handle(command, CancellationToken.None));
            _jobbApplicationRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            _jobbApplicationRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<JobbApplicationViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
