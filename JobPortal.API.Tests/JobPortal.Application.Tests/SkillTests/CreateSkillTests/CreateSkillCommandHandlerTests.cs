using System;
using AutoMapper;
using FluentAssertions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Skills.Commands.CreateSkill;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.SkillTests.CreateSkillTests
{
    public class CreateSkillCommandHandlerTests
    {
        [Fact]
        public async Task ReturnTheSkillId()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateSkillCommand
            {
                Name = "C#",
                YearsOfExperience = "5"
            };

            var jobSeeker = new JobSeeker { Id = 1 };
            var skill = new Skill { Id = 10, Name = command.Name, YearsOfExperience = command.YearsOfExperience, JobSeekerId = jobSeeker.Id };

            claimsPrincipalAccessorMock.Setup(x => x.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            mapperMock.Setup(m => m.Map<Skill>(command))
                .Returns(skill);

            unitOfWorkMock.Setup(u => u.Repository<Skill>().CreateAsync(skill))
                .Returns(Task.CompletedTask);

            unitOfWorkMock.Setup(u => u.Complete())
                .Returns(true);

            var handler = new CreateSkillCommandHandler(unitOfWorkMock.Object, claimsPrincipalAccessorMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(skill.Id);
            unitOfWorkMock.Verify(u => u.Repository<Skill>().CreateAsync(skill), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }

}