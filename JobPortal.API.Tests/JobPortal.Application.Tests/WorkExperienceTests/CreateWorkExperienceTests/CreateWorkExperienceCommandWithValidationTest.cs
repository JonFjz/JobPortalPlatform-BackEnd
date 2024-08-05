using FluentValidation.TestHelper;
using JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience.Validator;

public class CreateWorkExperienceCommandValidatorTest
{
    private readonly CreateWorkExperienceCommandValidator _validator;

    public CreateWorkExperienceCommandValidatorTest()
    {
        _validator = new CreateWorkExperienceCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenJobTitleIsNull()
    {
        var command = new CreateWorkExperienceCommand { JobTitle = null };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.JobTitle);
    }

    [Fact]
    public void ShouldHaveErrorWhenJobTitleIsTooLong()
    {
        var command = new CreateWorkExperienceCommand { JobTitle = new string('A', 101) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.JobTitle);
    }

    [Fact]
    public void ShouldHaveErrorWhenCompanyNameIsNull()
    {
        var command = new CreateWorkExperienceCommand { CompanyName = null };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompanyName);
    }

    [Fact]
    public void ShouldHaveErrorWhenCompanyNameIsTooLong()
    {
        var command = new CreateWorkExperienceCommand { CompanyName = new string('A', 51) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CompanyName);
    }

    [Fact]
    public void ShouldNotHaveErrorsWhenAllFieldsAreValid()
    {
        var command = new CreateWorkExperienceCommand
        {
            JobTitle = "Software Engineer",
            CompanyName = "Tech Corp"
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}