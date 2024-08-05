using AutoMapper;
using JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

public class CreateWorkExperienceCommandHandlerTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateWorkExperienceCommandHandler _handler;

    public CreateWorkExperienceCommandHandlerTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateWorkExperienceCommandHandler(
            _unitOfWorkMock.Object,
            _claimsPrincipalAccessorMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldCreateWorkExperience()
    {
        // Arrange
        var jobSeekerId = 1;
        var command = new CreateWorkExperienceCommand
        {
            JobTitle = "Software Engineer",
            CompanyName = "Tech Corp"
        };

        var jobSeeker = new JobSeeker { Id = jobSeekerId };
        var workExperienceToCreate = new WorkExperience { Id = 1 };

        _claimsPrincipalAccessorMock
            .Setup(a => a.GetCurrentJobSeekerAsync())
            .ReturnsAsync(jobSeeker);

        _mapperMock
            .Setup(m => m.Map<WorkExperience>(It.IsAny<CreateWorkExperienceCommand>()))
            .Returns(workExperienceToCreate);

        _unitOfWorkMock
            .Setup(u => u.Repository<WorkExperience>().CreateAsync(It.IsAny<WorkExperience>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.Complete())
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<WorkExperience>(command), Times.Once);
        _unitOfWorkMock.Verify(u => u.Repository<WorkExperience>().CreateAsync(workExperienceToCreate), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        Assert.Equal(workExperienceToCreate.Id, result);
    }

    [Fact]
    public async Task Handle_WhenExceptionOccurs_ShouldThrowException()
    {
        // Arrange
        var jobSeekerId = 1;
        var command = new CreateWorkExperienceCommand
        {
            JobTitle = "Software Engineer",
            CompanyName = "Tech Corp"
        };

        var jobSeeker = new JobSeeker { Id = jobSeekerId };
        var workExperienceToCreate = new WorkExperience { Id = 1 };

        _claimsPrincipalAccessorMock
            .Setup(a => a.GetCurrentJobSeekerAsync())
            .ReturnsAsync(jobSeeker);

        _mapperMock
            .Setup(m => m.Map<WorkExperience>(It.IsAny<CreateWorkExperienceCommand>()))
            .Returns(workExperienceToCreate);

        _unitOfWorkMock
            .Setup(u => u.Repository<WorkExperience>().CreateAsync(It.IsAny<WorkExperience>()))
            .Throws(new Exception("Database error"));

        _unitOfWorkMock
            .Setup(u => u.Complete())
            .Returns(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Database error", exception.Message);
    }
}