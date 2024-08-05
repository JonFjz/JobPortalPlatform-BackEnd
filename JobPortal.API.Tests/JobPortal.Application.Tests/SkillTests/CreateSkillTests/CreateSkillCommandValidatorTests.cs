using System;
using FluentValidation.TestHelper;
using JobPortal.Application.Features.Skills.Commands.CreateSkill;
using JobPortal.Application.Features.Skills.Commands.CreateSkill.Validator;

namespace JobPortal.Application.Tests.SkillTests.CreateSkillTests
{
	public class CreateSkillCommandValidatorTests
	{
        private readonly CreateSkillCommandValidator _validator;

        public CreateSkillCommandValidatorTests()
        {
            _validator = new CreateSkillCommandValidator();
        }

        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateSkillCommand
            {
                Name = "C#",
                YearsOfExperience = "5"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateSkillCommand
            {
                Name = "",
                YearsOfExperience = "five"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.YearsOfExperience);
        }
    }
}
