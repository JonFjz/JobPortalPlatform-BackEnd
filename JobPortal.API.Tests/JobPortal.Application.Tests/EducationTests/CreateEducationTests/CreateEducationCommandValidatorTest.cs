using System;
using FluentValidation.TestHelper;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Application.Features.Educations.Commands.CreateEducation.Validator;

namespace JobPortal.Application.Tests.EducationTests.CreateEducationTests
{
    public class CreateEducationCommandValidatorTest
    {
        private readonly CreateEducationCommandValidator _validator;

        public CreateEducationCommandValidatorTest()
        {
            _validator = new CreateEducationCommandValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenDegreeIsNull()
        {
            var command = new CreateEducationCommand { Degree = null };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Degree);
        }

        [Fact]
        public void ShouldHaveErrorWhenDegreeIsTooLong()
        {
            var command = new CreateEducationCommand { Degree = new string('A', 101) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Degree);
        }

        [Fact]
        public void ShouldHaveErrorWhenFieldOfStudyIsNull()
        {
            var command = new CreateEducationCommand { FieldOfStudy = null };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FieldOfStudy);
        }

        [Fact]
        public void ShouldHaveErrorWhenFieldOfStudyIsTooLong()
        {
            var command = new CreateEducationCommand { FieldOfStudy = new string('A', 101) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FieldOfStudy);
        }

        [Fact]
        public void ShouldHaveErrorWhenInstitutionNameIsNull()
        {
            var command = new CreateEducationCommand { InstitutionName = null };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.InstitutionName);
        }

        [Fact]
        public void ShouldHaveErrorWhenInstitutionNameIsTooLong()
        {
            var command = new CreateEducationCommand { InstitutionName = new string('A', 201) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.InstitutionName);
        }

        [Fact]
        public void ShouldNotHaveErrorsWhenAllFieldsAreValid()
        {
            var command = new CreateEducationCommand
            {
                Degree = "Bachelor's",
                FieldOfStudy = "Computer Science",
                InstitutionName = "University X"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}