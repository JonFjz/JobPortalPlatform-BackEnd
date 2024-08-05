using System;
using System.Linq.Expressions;
using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Educations.Commands.UpdateEducation;
using JobPortal.Domain.Entities;
using Moq;
using Xunit;

namespace JobPortal.API.Tests.JobPortal.Application.Tests.EducationTests.UpdateEducationTests
{
    public class UpdateEducationCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateEducationCommandHandler _handler;

        public UpdateEducationCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateEducationCommandHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _claimsPrincipalAccessorMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateEducation()
        {
            // Arrange
            var educationId = 1;
            var command = new UpdateEducationCommand
            {
                Id = educationId,
                Degree = "Bachelor",
                FieldOfStudy = "Computer Science",
                InstitutionName = "University X"
            };

            var jobSeeker = new JobSeeker { Id = 1 };
            var education = new Education
            {
                Id = educationId,
                JobSeekerId = jobSeeker.Id
            };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Education>().GetByIdAsync(It.IsAny<Expression<Func<Education, bool>>>()))
                .ReturnsAsync(education);

            _mapperMock
                .Setup(m => m.Map(command, education))
                .Callback<UpdateEducationCommand, Education>((cmd, edu) =>
                {
                    edu.Degree = cmd.Degree;
                    edu.FieldOfStudy = cmd.FieldOfStudy;
                    edu.InstitutionName = cmd.InstitutionName;
                });

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Repository<Education>().UpdateAsync(education), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }
}
