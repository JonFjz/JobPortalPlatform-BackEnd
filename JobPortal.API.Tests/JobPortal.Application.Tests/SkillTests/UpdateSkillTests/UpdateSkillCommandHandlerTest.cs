using System;
using System.Linq.Expressions;
using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Skills.Commands.UpdateSkill;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.SkillTests.UpdateSkillTests
{
    public class UpdateSkillCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateSkillCommandHandler _handler;

        public UpdateSkillCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateSkillCommandHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _claimsPrincipalAccessorMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateSkill()
        {
            // Arrange
            var skillId = 1;
            var jobSeekerId = 2;
            var command = new UpdateSkillCommand
            {
                Id = skillId,
                Name = "New Skill",
                YearsOfExperience = "5"
            };

            var jobSeeker = new JobSeeker { Id = jobSeekerId };
            var existingSkill = new Skill { Id = skillId, JobSeekerId = jobSeekerId };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Skill>().GetByIdAsync(It.IsAny<Expression<Func<Skill, bool>>>()))
                .ReturnsAsync(existingSkill);

            _mapperMock
                .Setup(m => m.Map(It.IsAny<UpdateSkillCommand>(), It.IsAny<Skill>()))
                .Callback<UpdateSkillCommand, Skill>((cmd, skill) =>
                {
                    skill.Name = cmd.Name;
                    skill.YearsOfExperience = cmd.YearsOfExperience;
                });

            _unitOfWorkMock
                .Setup(u => u.Repository<Skill>().UpdateAsync(It.IsAny<Skill>()))
                .Verifiable();

            _unitOfWorkMock
                .Setup(u => u.Complete())
                .Verifiable();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Repository<Skill>().UpdateAsync(existingSkill), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
            Assert.Equal("New Skill", existingSkill.Name);
            Assert.Equal("5", existingSkill.YearsOfExperience);
        }

        [Fact]
        public async Task Handle_WithSkillNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var skillId = 1;
            var command = new UpdateSkillCommand
            {
                Id = skillId,
                Name = "New Skill",
                YearsOfExperience = "5"
            };

            var jobSeeker = new JobSeeker { Id = 2 };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Skill>().GetByIdAsync(It.IsAny<Expression<Func<Skill, bool>>>()))
                .ReturnsAsync((Skill)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Education not found.", exception.Message);
        }

        [Fact]
        public async Task Handle_WithUnauthorizedSkill_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var skillId = 1;
            var command = new UpdateSkillCommand
            {
                Id = skillId,
                Name = "New Skill",
                YearsOfExperience = "5"
            };

            var jobSeeker = new JobSeeker { Id = 2 };
            var unauthorizedSkill = new Skill { Id = skillId, JobSeekerId = jobSeeker.Id + 1 };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Skill>().GetByIdAsync(It.IsAny<Expression<Func<Skill, bool>>>()))
                .ReturnsAsync(unauthorizedSkill);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Education not found.", exception.Message);
        }
    }
}

