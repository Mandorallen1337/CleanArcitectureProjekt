using Application.Interfaces.RepoInterface;
using Application.Queries.CVQueries;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject.CvTest
{
    [TestFixture]
    public class CVQueryHandlersTests
    {
        private Mock<IRepository<CV>> _cvRepositoryMock;
        private GetAllCVsQueryHandler _getAllHandler;
        private GetCVByIdQueryHandler _getByIdHandler;
        private Mock<ILogger<GetAllCVsQueryHandler>> _loggerGetAllMock;
        private Mock<ILogger<GetCVByIdQueryHandler>> _loggerGetByIdMock;

        [SetUp]
        public void Setup()
        {
            _cvRepositoryMock = new Mock<IRepository<CV>>();
            _loggerGetAllMock = new Mock<ILogger<GetAllCVsQueryHandler>>();
            _loggerGetByIdMock = new Mock<ILogger<GetCVByIdQueryHandler>>();

            _getAllHandler = new GetAllCVsQueryHandler(_cvRepositoryMock.Object, _loggerGetAllMock.Object);
            _getByIdHandler = new GetCVByIdQueryHandler(_cvRepositoryMock.Object, _loggerGetByIdMock.Object);
        }

        
        [Test]
        public async Task Handle_ShouldReturnAllCVs_WhenCVsExist()
        {
            // Arrange
            var cvList = new List<CV>
            {
                new CV { Id = Guid.NewGuid(), FileName = "cv1.pdf", FileUrl = "https://example.com/cv1.pdf" },
                new CV { Id = Guid.NewGuid(), FileName = "cv2.pdf", FileUrl = "https://example.com/cv2.pdf" }
            };

            _cvRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cvList);

            // Act
            var result = await _getAllHandler.Handle(new GetAllCVsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("cv1.pdf", result[0].FileName);
            Assert.AreEqual("cv2.pdf", result[1].FileName);

            _cvRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _loggerGetAllMock.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                null,
                (Func<object, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCVsExist()
        {
            _cvRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<CV>());

            var result = await _getAllHandler.Handle(new GetAllCVsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);

            _cvRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _loggerGetAllMock.Verify(x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                null,
                (Func<object, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        
        [Test]
        public async Task Handle_ShouldReturnCV_WhenCVExists()
        {
            var cvId = Guid.NewGuid();
            var cv = new CV { Id = cvId, FileName = "cv.pdf", FileUrl = "https://example.com/cv.pdf" };

            _cvRepositoryMock.Setup(r => r.GetByIdAsync(cvId, It.IsAny<CancellationToken>())).ReturnsAsync(cv);

            var result = await _getByIdHandler.Handle(new GetCVByIdQuery(cvId), CancellationToken.None);

            Assert.NotNull(result);
            Assert.AreEqual(cvId, result.Id);
            Assert.AreEqual("cv.pdf", result.FileName);

            _cvRepositoryMock.Verify(r => r.GetByIdAsync(cvId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenCVDoesNotExist()
        {
            var cvId = Guid.NewGuid();

            _cvRepositoryMock.Setup(r => r.GetByIdAsync(cvId, It.IsAny<CancellationToken>())).ReturnsAsync((CV)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _getByIdHandler.Handle(new GetCVByIdQuery(cvId), CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"CV with ID {cvId} was not found."));

            _loggerGetByIdMock.Verify(x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                null,
                (Func<object, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

       
        [Test]
        public void Handle_ShouldLogError_WhenGetAllAsyncFails()
        {
            _cvRepositoryMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _getAllHandler.Handle(new GetAllCVsQuery(), CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo("Database error"));

            _loggerGetAllMock.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                (Func<object, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
