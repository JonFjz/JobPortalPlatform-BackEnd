using System;
using FluentAssertions;
using JobPortal.API.Controllers;
using JobPortal.Application.Features.Skills.Dtos;
using JobPortal.Application.Features.Skills.Queries.GetAllSkills;
using JobPortal.Application.Features.Skills.Queries.GetSkillById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobPortal.API.Tests.Controllers
{
    public class SkillControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly SkillsController _controller;

        public SkillControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new SkillsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithSkillDto()
        {
            // Arrange
            var skillId = 1;
            var skillDto = new SkillDto { Id = skillId, Name = "C#", YearsOfExperience = "5" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetSkillByIdQuery>(), default))
                .ReturnsAsync(skillDto);

            // Act
            var result = await _controller.GetById(skillId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(skillDto);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfSkillDto()
        {
            // Arrange
            var skillDtos = new List<SkillDto>
            {
                new SkillDto { Id = 1, Name = "C#", YearsOfExperience = "5" },
                new SkillDto { Id = 2, Name = "JavaScript", YearsOfExperience = "3" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllSkillsQuery>(), default))
                .ReturnsAsync(skillDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(skillDtos);
        }
    }
}