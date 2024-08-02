using FluentValidation;

namespace JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience.Validator
{
    public class CreateWorkExperienceCommandValidator : AbstractValidator<CreateWorkExperienceCommand>
    {
        public CreateWorkExperienceCommandValidator()
        {
            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job Title is required.")
                .MaximumLength(100).WithMessage("Job Title must not exceed 100 characters.");

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company Name is required.")
                .MaximumLength(50).WithMessage("Company Name must not exceed 50 characters.");
        }
    }
}