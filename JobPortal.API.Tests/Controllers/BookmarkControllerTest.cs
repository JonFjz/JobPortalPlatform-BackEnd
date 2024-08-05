using System;
using FluentAssertions;
using JobPortal.API.Controllers;
using JobPortal.Application.Features.BookmarkJobs.Dtos;
using JobPortal.Application.Features.BookmarkJobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace JobPortal.API.Tests.Controllers
{
    public class BookmarkControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BookmarkJobsController _controller;

        public BookmarkControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookmarkJobsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetBookmarkedJobs_ShouldReturnOkResult_WithListOfBookmarkedJobs()
        {
            // Arrange
            var bookmarkedJobs = new List<BookmarkedJobDto>
            {
                new BookmarkedJobDto { JobPostingId = 1, JobTitle = "Software Developer" },
                new BookmarkedJobDto { JobPostingId = 2, JobTitle = "Data Scientist" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookmarkedJobsQuery>(), default))
                .ReturnsAsync(bookmarkedJobs);

            // Act
            var result = await _controller.GetBookmarkedJobs();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnedJobs = okResult.Value as List<BookmarkedJobDto>;
            returnedJobs.Should().NotBeNull();
            returnedJobs.Should().HaveCount(2);
            returnedJobs[0].JobTitle.Should().Be("Software Developer");
            returnedJobs[1].JobTitle.Should().Be("Data Scientist");
        }
    }
}