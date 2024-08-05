using System;
using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Application.Features.s.Commands.CreateEducation;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.EducationTests.CreateEducationTests
{
	public class CreateEducationCommandHandlerTest
	{
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateEducationCommandHandler _handler;

        public CreateEducationCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateEducationCommandHandler(
                _unitOfWorkMock.Object,
                _claimsPrincipalAccessorMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldCreateEducation()
        {
            // Arrange
            var jobSeekerId = 1;
            var command = new CreateEducationCommand
            {
                Degree = "Bachelor's",
                FieldOfStudy = "Computer Science",
                InstitutionName = "University X"
            };

            var jobSeeker = new JobSeeker { Id = jobSeekerId };
            var educationToCreate = new Education();

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _mapperMock
                .Setup(m => m.Map<Education>(It.IsAny<CreateEducationCommand>()))
                .Returns(educationToCreate);

            _unitOfWorkMock
                .Setup(u => u.Repository<Education>().CreateAsync(It.IsAny<Education>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Education>(command), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<Education>().CreateAsync(educationToCreate), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
            Assert.Equal(educationToCreate.Id, result);
        }
    }
}


