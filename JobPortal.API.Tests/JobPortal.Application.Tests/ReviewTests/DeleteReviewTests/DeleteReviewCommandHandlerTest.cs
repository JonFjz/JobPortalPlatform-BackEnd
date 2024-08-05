using System;
using System.Linq.Expressions;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Reviews.Commands.DeleteReview;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Application.Tests.ReviewTests.DeleteReviewTests
{
    public class DeleteReviewCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IClaimsPrincipalAccessor> _claimsPrincipalAccessorMock;
        private readonly DeleteReviewCommandHandler _handler;

        public DeleteReviewCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimsPrincipalAccessorMock = new Mock<IClaimsPrincipalAccessor>();
            _handler = new DeleteReviewCommandHandler(
                _unitOfWorkMock.Object,
                _claimsPrincipalAccessorMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldDeleteReview()
        {
            // Arrange
            var reviewId = 1;
            var jobSeekerId = 2;
            var command = new DeleteReviewCommand(reviewId);

            var jobSeeker = new JobSeeker { Id = jobSeekerId };
            var reviewToDelete = new Review { Id = reviewId, JobSeekerId = jobSeekerId };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Review>().GetByIdAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .ReturnsAsync(reviewToDelete);

            _unitOfWorkMock
                .Setup(u => u.Repository<Review>().Delete(It.IsAny<Review>()))
                .Verifiable();

            _unitOfWorkMock
                .Setup(u => u.Complete())
                .Verifiable();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.Repository<Review>().Delete(reviewToDelete), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithInvalidReview_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var reviewId = 1;
            var jobSeekerId = 2;
            var command = new DeleteReviewCommand(reviewId);

            var jobSeeker = new JobSeeker { Id = jobSeekerId };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Review>().GetByIdAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .ReturnsAsync((Review)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Review not foud or you are not authorized!", exception.Message);
        }

        [Fact]
        public async Task Handle_WithUnauthorizedReview_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var reviewId = 1;
            var jobSeekerId = 2;
            var command = new DeleteReviewCommand(reviewId);

            var jobSeeker = new JobSeeker { Id = jobSeekerId };
            var unauthorizedReview = new Review { Id = reviewId, JobSeekerId = jobSeekerId + 1 };

            _claimsPrincipalAccessorMock
                .Setup(a => a.GetCurrentJobSeekerAsync())
                .ReturnsAsync(jobSeeker);

            _unitOfWorkMock
                .Setup(u => u.Repository<Review>().GetByIdAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .ReturnsAsync(unauthorizedReview);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Review not foud or you are not authorized!", exception.Message);
        }
    }

}