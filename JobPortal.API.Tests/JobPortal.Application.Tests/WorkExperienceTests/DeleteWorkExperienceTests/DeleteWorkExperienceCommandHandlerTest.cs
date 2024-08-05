using System;
using System.Linq.Expressions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.WorkExperienceTests.DeleteWorkExperienceTests
{
    public class DeleteWorkExperienceCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly DeleteWorkExperienceCommandHandler _handler;

        public DeleteWorkExperienceCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _handler = new DeleteWorkExperienceCommandHandler(_unitOfWorkMock.Object, _claimsPrincipalAccessorMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldDeleteWorkExperience()
        {
            // Arrange
            var workExperienceId = 1;
            var command = new DeleteWorkExperienceCommand(workExperienceId);

            var jobSeeker = new JobSeeker { Id = 1 };
            var workExperience = new WorkExperience
            {
                Id = workExperienceId,
                JobSeekerId = jobSeeker.Id
            };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<WorkExperience>().GetByIdAsync(It.IsAny<Expression<Func<WorkExperience, bool>>>()))
                .ReturnsAsync(workExperience);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Repository<WorkExperience>().Delete(workExperience), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }
}