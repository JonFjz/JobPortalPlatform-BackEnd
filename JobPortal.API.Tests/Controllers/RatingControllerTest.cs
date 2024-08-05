using System;
using FluentAssertions;
using JobPortal.API.Controllers;
using JobPortal.Application.Features.Ratings.Commands.CreateRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobPortal.API.Tests.Controllers
{
	public class RatingControllerTest
	{
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RatingController _controller;

        public RatingControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RatingController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddRating_ShouldReturnOkResult()
        {
            // Arrange
            var command = new CreateRatingCommand { EmployerId = 1, Score = 5 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRatingCommand>(), default))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddRating(command);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }
    }
}
