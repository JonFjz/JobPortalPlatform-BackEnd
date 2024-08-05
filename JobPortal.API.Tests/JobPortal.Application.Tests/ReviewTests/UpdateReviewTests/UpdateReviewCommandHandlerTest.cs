using System;
using System.Linq.Expressions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Reviews.Commands.UpdateReview;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.ReviewTests.UpdateReviewTests
{
    public class UpdateReviewCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly UpdateReviewCommandHandler _handler;

        public UpdateReviewCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _handler = new UpdateReviewCommandHandler(_unitOfWorkMock.Object, _claimsPrincipalAccessorMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateReview()
        {
            // Arrange
            var reviewId = 1;
            var command = new UpdateReviewCommand
            {
                Id = reviewId,
                Content = "Updated Content"
            };

            var jobSeeker = new JobSeeker { Id = 1 };
            var review = new Review
            {
                Id = reviewId,
                JobSeekerId = jobSeeker.Id,
                Content = "Old Content"
            };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Review>().GetByIdAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .ReturnsAsync(review);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Repository<Review>().UpdateAsync(It.Is<Review>(r => r.Content == "Updated Content")), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }
}