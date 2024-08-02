using FluentValidation;

namespace JobPortal.Application.Features.Skills.Commands.CreateSkill.Validator
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.YearsOfExperience)
                .NotEmpty().WithMessage("Years of Experience is required.")
                .Matches(@"^\d+(\.\d+)?$").WithMessage("Years of Experience must be a number.");
        }
    }
}

