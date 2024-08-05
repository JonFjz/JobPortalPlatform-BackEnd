using System;
using System.Linq.Expressions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Ratings.Commands.CreateRating;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.RatingTests.CreateRatingTests
{
    public class CreateRatingCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ForValidCommand_CreatesRatingSuccessfully()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            var currentJobSeeker = new JobSeeker { Id = 1 };
            var employer = new Employer { Id = 1 };

            claimsPrincipalAccessorMock
                .Setup(c => c.GetCurrentJobSeekerAsync())
                .ReturnsAsync(currentJobSeeker);

            unitOfWorkMock
                .Setup(u => u.Repository<Employer>().GetByIdAsync(It.IsAny<Expression<Func<Employer, bool>>>()))
                .ReturnsAsync(employer);

            unitOfWorkMock
                .Setup(u => u.Repository<Rating>().CreateAsync(It.IsAny<Rating>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.Complete())
                .Returns(true);

            var command = new CreateRatingCommand { EmployerId = 1, Score = 5 };
            var commandHandler = new CreateRatingCommandHandler(unitOfWorkMock.Object, claimsPrincipalAccessorMock.Object);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.Repository<Rating>().CreateAsync(It.Is<Rating>(r =>
                r.JobSeekerId == currentJobSeeker.Id &&
                r.EmployerId == command.EmployerId &&
                r.Score == command.Score
            )), Times.Once);

            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenEmployerDoesNotExist_ThrowsException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            var currentJobSeeker = new JobSeeker { Id = 1 };

            claimsPrincipalAccessorMock
                .Setup(c => c.GetCurrentJobSeekerAsync())
                .ReturnsAsync(currentJobSeeker);

            unitOfWorkMock
                .Setup(u => u.Repository<Employer>().GetByIdAsync(It.IsAny<Expression<Func<Employer, bool>>>()))
                .ReturnsAsync((Employer)null);

            var command = new CreateRatingCommand { EmployerId = 1, Score = 5 };
            var commandHandler = new CreateRatingCommandHandler(unitOfWorkMock.Object, claimsPrincipalAccessorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ThrowsExceptionWithCustomMessage()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            var currentJobSeeker = new JobSeeker { Id = 1 };
            var employer = new Employer { Id = 1 };

            claimsPrincipalAccessorMock
                .Setup(c => c.GetCurrentJobSeekerAsync())
                .ReturnsAsync(currentJobSeeker);

            unitOfWorkMock
                .Setup(u => u.Repository<Employer>().GetByIdAsync(It.IsAny<Expression<Func<Employer, bool>>>()))
                .ReturnsAsync(employer);

            unitOfWorkMock
                .Setup(u => u.Repository<Rating>().CreateAsync(It.IsAny<Rating>()))
                .Throws(new Exception());

            var command = new CreateRatingCommand { EmployerId = 1, Score = 5 };
            var commandHandler = new CreateRatingCommandHandler(unitOfWorkMock.Object, claimsPrincipalAccessorMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("A rating from this job seeker for this employer already exists.", exception.Message);
        }
    }
}
