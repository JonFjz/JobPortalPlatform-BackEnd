using System;
using FluentValidation.TestHelper;
using JobPortal.Application.Features.Ratings.Commands.CreateRating;
using JobPortal.Application.Features.Ratings.Commands.CreateRating.Validator;

namespace JobPortal.Application.Tests.RatingTests.CreateRatingTests
{
    public class CreateRatingCommandValidatorTests
    {
        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRatingCommand
            {
                EmployerId = 1,
                Score = 3
            };

            var validator = new CreateRatingCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRatingCommand
            {
                EmployerId = 0, // Invalid
                Score = 6 // Invalid
            };

            var validator = new CreateRatingCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.EmployerId);
            result.ShouldHaveValidationErrorFor(c => c.Score);
        }
    }
}
