using System;
using FluentAssertions;
using JobPortal.API.Controllers;
using JobPortal.Application.Features.Educations.Dtos;
using JobPortal.Application.Features.Educations.Queries.GetAllEducations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobPortal.API.Tests.Controllers
{
	public class EducationControllerTest
	{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EducationsController _controller;

        public EducationControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EducationsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithListOfEducationDto()
        {
            // Arrange
            var educationDtos = new List<EducationDto>
            {
                new EducationDto { Id = 1, Degree = "Bachelor of Science", FieldOfStudy = "Computer Science", InstitutionName = "University XYZ" },
                new EducationDto { Id = 2, Degree = "Master of Science", FieldOfStudy = "Data Science", InstitutionName = "University ABC" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEducationsQuery>(), default))
                         .ReturnsAsync(educationDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var returnedEducations = okResult.Value as List<EducationDto>;
            returnedEducations.Should().NotBeNull();
            returnedEducations.Should().BeEquivalentTo(educationDtos);
        }
    }
}