using System;
using FluentAssertions;
using JobPortal.API.Controllers;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobPortal.API.Tests.Controllers
{
	public class JobPostingControllerTest
	{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ISearchService> _searchServiceMock;
        private readonly JobPostingsController _controller;

        public JobPostingControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _searchServiceMock = new Mock<ISearchService>();
            _controller = new JobPostingsController(_mediatorMock.Object, _searchServiceMock.Object);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContentResult()
        {
            // Arrange
            var updateJobPostingCommand = new UpdateJobPostingCommand
            {
                Id = 1,
                Title = "Updated Job Title",
                ClosingDate = DateTime.Now.AddDays(30),
                Description = "Updated job description",
                Responsibilities = "Updated responsibilities",
                RequiredSkills = "Updated required skills",
                WorkType = "Full-Time",
                WorkLevel = "Senior",
                NotificationEmail = "updated@example.com"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateJobPostingCommand>(), default))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.Update(updateJobPostingCommand);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204); // No Content
        }
    }
}
