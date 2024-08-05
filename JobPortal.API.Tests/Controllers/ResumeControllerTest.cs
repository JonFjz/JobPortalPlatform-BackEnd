using System;
using JobPortal.API.Controllers;
using JobPortal.Application.Features.Resumes.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using MediatR;
using JobPortal.Application.Features.Resumes.Queries.GetResume;
using FluentAssertions;

namespace JobPortal.API.Tests.Controllers
{
    public class ResumeControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ResumesController _controller;

        public ResumeControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ResumesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResultWithResumeDto()
        {
            // Arrange
            var resumeDto = new ResumeDto
            {
                Id = 1,
                ResumeName = "My Resume",
                UploadedAt = DateTime.UtcNow
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetResumeQuery>(), default))
                .ReturnsAsync(resumeDto);

            // Act
            var result = await _controller.Get() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().Be(resumeDto);
        }
    }
}